using DinkToPdf;

namespace MailingNinja.Contracts.Services
{
    public interface IPdfService
    {
        byte[] Convert(string htmlContent, PechkinPaperSize paperSize);
    }
}