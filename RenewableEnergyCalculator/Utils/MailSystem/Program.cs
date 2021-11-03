
namespace MailSystem
{

    class Program
    {
        static void Main(string[] args)
        {
            Mail mail = new Mail();

            Recepient recipient = new Recepient("Alex", "barbualexandru14@yahoo.com");

            mail.setRecepient(recipient);

            mail.addAtachement(@"C:\Users\shank\source\repos\MailSystem\report.jpg");

            mail.sendMail();
        }
    }
}
