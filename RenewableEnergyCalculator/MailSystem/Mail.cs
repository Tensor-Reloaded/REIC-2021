using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MimeKit;

namespace RenewableEnergyCalculator.MailSystem
{
    public class Mail
    {
        private readonly Sender _sender = Sender.GetInstance();
        private Recepient _recepient;

        private readonly MimeMessage _message;
        private readonly BodyBuilder _body;

        private SMTPClient _mailClient;


        public Mail()
        {
            _message = new MimeMessage();

            _message.From.Add(new MailboxAddress(_sender.getName(), _sender.getEmailAddress()));

            _body = new BodyBuilder();
        }


        public bool TrySetRecipient(Recepient recepient)
        {
            _recepient = recepient;
            try
            {
                _message.To.Add(MailboxAddress.Parse(_recepient.GetEmailAddress()));
                return true;
            }
            catch (ParseException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool TrySetSubject()
        {
            try
            {
                //_message.Subject = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\EmailSubject.txt");
                _message.Subject = "Hello";
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool TrySetSubject(string subject)
        {
            if (subject == "")
                return false;

            _message.Subject = subject;
            return true;
        }

        public bool TrySetBody()
        {
            try
            {
                //_body.TextBody = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\EmailBody.txt");
                _body.TextBody = "Thank you";
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool TrySetBody(string body)
        {
            if (body == "")
                return false;

            _body.TextBody = body;
            return true;
        }

        public bool TryAddAtachement(string pathToAttachement)
        {
            try
            {
                Path.GetFullPath(pathToAttachement);
                _body.Attachments.Add(pathToAttachement);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool TrySendEmail()
        {
            _message.Body = _body.ToMessageBody();
            _mailClient = new SMTPClient();

            try
            {
                _mailClient.server.Connect(_mailClient.GetName(), _mailClient.GetPort(), _mailClient.GetEnableSSL());
                _mailClient.server.Authenticate(_sender.getEmailAddress(), _sender.getPassword());
                _mailClient.server.Send(_message);

                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _mailClient.server.Disconnect(true);
                _mailClient.server.Dispose();
            }

            return true;
        }

    }
}