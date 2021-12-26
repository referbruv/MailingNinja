using Microsoft.AspNetCore.Mvc;
using MediatR;
using MailingNinja.Core.Handlers.Items.Queries;
using MailingNinja.Contracts.Services;
using DinkToPdf;
using MailingNinja.Core.Contracts.DTO;
using MailingNinja.Contracts.DTO;
using System.Net.Mime;

namespace MailingNinja.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;
    private readonly ITemplateService _templateService;
    private readonly IPdfService _pdfService;
    private readonly IMailingService _mailingService;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, IMediator mediator, ITemplateService templateService, IPdfService pdfService, IMailingService mailingService, IWebHostEnvironment environment)
    {
        _logger = logger;
        _mediator = mediator;
        _templateService = templateService;
        _pdfService = pdfService;
        _mailingService = mailingService;
        _environment = environment;
    }

    public async Task<IActionResult> IndexAsync(DateTime tillDate, string doAction = "", string emailAddress = "")
    {
        if (tillDate == DateTime.MinValue)
        {
            tillDate = DateTime.Now;
        }

        var getAllNinjasTillToday = new GetAllForGivenDate(tillDate);
        var data = await _mediator.Send(getAllNinjasTillToday);

        if (doAction == "pdf")
        {
            return await DownloadPdfContentAsync(data);
        }
        else if (doAction == "mail")
        {
            await SendReportMailAsync(data, emailAddress);

            // await SendReportMailWithPdfAttachmentAsync(data, emailAddress);
        }

        return View(data);
    }

    private async Task SendReportMailAsync(IEnumerable<NinjaDTO> data, string emailAddress)
    {
        var content = await GetGridContentAsync(data);
        _mailingService.SendSimple(emailAddress, "Your Report", content);
    }

    private async Task SendReportMailWithPdfAttachmentAsync(IEnumerable<NinjaDTO> data, string emailAddress)
    {
        var headerImagePath = string.Format("{0}/{1}", _environment.WebRootPath, "images/mail-header-solid.png");

        var mailingModel = new MailContentDTO();

        mailingModel.HeaderImage = new LinkedResource
        {
            ContentId = "header",
            ContentPath = headerImagePath,
            ContentType = "image/png"
        };

        mailingModel.Attachment = new LinkedResource
        {
            ContentId = $"ninja_list_{DateTime.UtcNow.Ticks}.pdf",
            ContentType = MediaTypeNames.Application.Pdf,
            ContentBytes = await GetPdfContentBytesAsync(data)
        };

        mailingModel.HtmlContent = await GetGridContentAsync(data);
        _mailingService.Send(emailAddress, "Your Report", mailingModel);
    }

    private async Task<IActionResult> DownloadPdfContentAsync(IEnumerable<NinjaDTO> data)
    {
        var pdfBytes = await GetPdfContentBytesAsync(data);

        MemoryStream ms = new MemoryStream();
        ms.Write(pdfBytes, 0, pdfBytes.Length);
        ms.Position = 0;

        return File(ms, "application/pdf", $"ninja_list_{DateTime.UtcNow.Ticks}.pdf");
    }

    private async Task<string> GetGridContentAsync(IEnumerable<NinjaDTO> data)
    {
        var templatePath = "MailingTemplates/List";
        return await _templateService.GetTemplateHtmlAsStringAsync(templatePath, data.ToList());
    }

    private async Task<byte[]> GetPdfContentBytesAsync(IEnumerable<NinjaDTO> data)
    {
        var gridContent = await GetGridContentAsync(data);
        var pdfBytes = _pdfService.Convert(gridContent, PaperKind.A4);
        return pdfBytes;
    }
}
