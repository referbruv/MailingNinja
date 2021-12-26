using MailingNinja.Contracts.Services;
using MailingNinja.Core.Contracts;
using MailingNinja.Core.Contracts.DTO;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailingNinja.Core.Services
{
    public class MailingService : IMailingService
    {
        private readonly MailServerConfig _mailServerConfig;

        public MailingService(MailServerConfig mailServerConfig)
        {
            _mailServerConfig = mailServerConfig;
        }

        public void Send(string to, string subject, MailContentDTO messageContent)
        {
            try
            {
                string fromAddress = _mailServerConfig.FromAddress;
                string serverAddress = _mailServerConfig.ServerAddress;
                string username = _mailServerConfig.Username;
                string password = _mailServerConfig.Password;
                int port = _mailServerConfig.Port;
                bool isSsl = _mailServerConfig.IsUseSsl;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromAddress, fromAddress));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                // create a new instance of the BodyBuilder class
                var builder = new BodyBuilder();

                if (messageContent.HeaderImage != null)
                {
                    // add a header image linked resource to the builder
                    var header = builder.LinkedResources.Add(messageContent.HeaderImage.ContentPath);

                    // set the contentId for the resource added to the builder to the contentId passed
                    header.ContentId = messageContent.HeaderImage.ContentId;
                }

                // set the html body (since we are passing html content to render) to the body of the builder
                builder.HtmlBody = messageContent.HtmlContent;

                if (messageContent.Attachment != null)
                {
                    var attachment = messageContent.Attachment;
                    builder.Attachments.Add(attachment.ContentId, attachment.ContentBytes, ContentType.Parse(attachment.ContentType));
                }

                // convert all the configuration made into a message content which is assigned to the message body
                message.Body = builder.ToMessageBody();


                using (var client = new SmtpClient())
                {
                    // connect
                    client.Timeout = 30000;
                    client.Connect(serverAddress, port, isSsl);
                    Console.WriteLine("Connected");

                    // authenticate
                    client.Authenticate(username, password);
                    Console.WriteLine("Authenticated");

                    // send message
                    client.Send(message);
                    Console.WriteLine("Sent");

                    // disconnect
                    client.Disconnect(true);
                    Console.WriteLine("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void SendSimple(string to, string subject, string messageContent)
        {
            try
            {
                string fromAddress = _mailServerConfig.FromAddress;
                string serverAddress = _mailServerConfig.ServerAddress;
                string username = _mailServerConfig.Username;
                string password = _mailServerConfig.Password;
                int port = _mailServerConfig.Port;
                bool isSsl = _mailServerConfig.IsUseSsl;

                var message = new MimeMessage();

                // the first arg is a name, but we pass
                // the emailAddress in both the places
                message.From.Add(new MailboxAddress(
                    fromAddress, fromAddress));

                // the first arg is a name, but we pass
                // the emailAddress in both the places
                message.To.Add(new MailboxAddress(to, to));
                
                message.Subject = subject;

                // assign the message content
                // to the message body
                message.Body = new TextPart("html")
                {
                    Text = messageContent
                };

                using (var client = new SmtpClient())
                {
                    // connect
                    client.Timeout = 30000;
                    client.Connect(serverAddress, port, isSsl);
                    Console.WriteLine("Connected");

                    // authenticate
                    client.Authenticate(username, password);
                    Console.WriteLine("Authenticated");

                    // send message
                    client.Send(message);
                    Console.WriteLine("Sent");

                    // disconnect
                    client.Disconnect(true);
                    Console.WriteLine("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}