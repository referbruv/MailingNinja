using MailingNinja.Core.Contracts.DTO;

namespace MailingNinja.Contracts.Services
{
    public interface IMailingService
    {
        void SendMail(string to, string subject, MailContentDTO model);
    }
}