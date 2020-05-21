using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorViewEngineApp.Providers;

namespace RazorViewEngineApp.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly ITemplateHelper _templateHelper;

        public TemplatesController(ITemplateHelper helper)
        {
            _templateHelper = helper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _templateHelper.GetTemplateHtmlAsStringAsync<List<Reader>>("Templates/Content", ReaderStore.Readers);
            return View("Index", response);
        }
    }
}