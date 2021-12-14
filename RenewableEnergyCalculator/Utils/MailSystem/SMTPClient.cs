/// ////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SMTPClient.cs
//FileType: Visual C# Source file
//Author: Barbu Alexandru
//Description: Class that models the mail client used for sending the e-mail.
//////////////////////////////////////////////////////////////////////////////////////

using MailKit.Net.Smtp;

namespace MailSystem
{
    public class SMTPClient
    {
        // name of the smtp provider
        private string name = "smtp.gmail.com";
        // port for secure SMTP
        private int port = 465;
        // option when connecting to the mail server
        private bool enableSSL = true;
        // the mail server
        public SmtpClient server = new SmtpClient();

        public string getName()
        {
            return name;
        }

        public int getPort()
        {
            return port;
        }

        public bool getEnableSSL()
        {
            return enableSSL;
        }
    }
}
