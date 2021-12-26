using MailingNinja.Core.Contracts.DTO;

namespace MailingNinja.Contracts.Services
{
    public interface IMailingService
    {
        void Send(string to, string subject, MailContentDTO model);

        void SendSimple(string to, string subject, string messageContent);
    }
}