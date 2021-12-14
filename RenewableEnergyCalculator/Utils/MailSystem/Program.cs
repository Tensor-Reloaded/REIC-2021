using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;

namespace MailSystem
{

    class Program
    {
        static void Main()
        {
            var backend = new ConsoleLoggingBackend();
            LoggingServices.DefaultBackend = backend;

            Mail mail = new Mail();

            //Recepient recipient = new Recepient("Alex", "barbualexandru14@yahoo.com");

            //mail.TrySetRecipient(recipient);

            mail.TrySetSubject();

            mail.TrySetBody();

            mail.TryAddAtachement(@"C:\Users\shank\source\repos\MailSystem\report.jpg");

            mail.TrySendEmail();
        }
    }
}
