/// ////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Sender.cs
//FileType: Visual C# Source file
//Author: Barbu Alexandru
//Description: Class that models the sender of the e-mail. Uses Singleton design pattern.
//////////////////////////////////////////////////////////////////////////////////////

namespace MailSystem
{
    public sealed class Sender
    {
        // Used for implementing Singleton
        private static Sender _instance;

        // The name of the sender. It will apper in the mail
        private string _name = "REIC Team";
        // The e-mail address used to send mails
        private string _emailAddress = "renewableenergycalculator@gmail.com";
        // The e-mail password. Will be read from a file
        private string _password = "";

        /// <summary>
        /// The constructor of the Sender class. It is private in order to maintain only a instance of this object.
        /// The password is read from a file. 
        /// </summary>
        private Sender()
        {
            _password = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\password.txt");
        }

        public static Sender GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Sender();
            }
            return _instance;
        }

        public string getName()
        {
            return _name;
        }

        public string getEmailAddress()
        {
            return _emailAddress;
        }

        public string getPassword()
        {
            return _password;
        }

    }
}
