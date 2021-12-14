/// ////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Recipient.cs
//FileType: Visual C# Source file
//Author: Barbu Alexandru
//Description: Class that models the recipient of the e-mail.
//////////////////////////////////////////////////////////////////////////////////////

namespace MailSystem
{
    public class Recepient
    {
        // default name of the recipient
        private string _name = "Teo";
        // default address of the recipient
        private string _emailAddress = "teodora.hoamea@gmail.com";

        public Recepient(string name, string emailAddress)
        {
            _name = name;
            _emailAddress = emailAddress;
        }

        public string getEmailAddress()
        {
            return _emailAddress;
        }

        public void setEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public void setName(string name)
        {
            _name = name;
        }

        public string getName()
        {
            return _name;
        }
    }
}