using MimeKit;
using System.Collections.Generic;
using System;


namespace MailSystem
{
    class Mail
    {
        private Sender _sender = Sender.GetInstance();
        private Recepient _recepient;

        private MimeMessage _message;
        private BodyBuilder _body;
        private List<string> attachements = new List<string>();

        private SMTPClient _mailClient;


        public Mail()
        {
            _message = new MimeMessage();

            _message.From.Add(new MailboxAddress(_sender.getName(), _sender.getEmailAddress()));

            _message.Subject = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\EmailSubject.txt");
        }


        public void setRecepient(Recepient recepient)
        {
            _recepient = recepient;
            _message.To.Add(MailboxAddress.Parse(_recepient.getEmailAddress()));
        }

        public void addAtachement(String pathToAtachementFile)
        {
            attachements.Add(pathToAtachementFile);
        }


        private void composeBody()
        {
            _body = new BodyBuilder();
            _body.TextBody = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\EmailBody.txt");

            foreach (String attachement in attachements)
            {
                _body.Attachments.Add(attachement);
            }

            _message.Body = _body.ToMessageBody();
        }

        public void sendMail()
        {
            composeBody();

            _mailClient = new SMTPClient();

            try
            {
                _mailClient.server.Connect(_mailClient.getName(), _mailClient.getPort(), _mailClient.getEnableSSL());
                _mailClient.server.Authenticate(_sender.getEmailAddress(), _sender.getPassword());
                _mailClient.server.Send(_message);

                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                _mailClient.server.Disconnect(true);
                _mailClient.server.Dispose();
            }

        }
    }
}
