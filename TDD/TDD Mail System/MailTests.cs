using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSystem;

namespace MailSystemTests
{
    [TestClass]
    public class MailTests
    {
        [TestMethod]
        public void trySetRecipient_RealRecepient_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();
            Recepient recepient = new Recepient("Alex", "barbugalexandru@gmail.com");

            //Act
            bool result = mail.trySetRecipient(recepient);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void trySetSubject_DefaultSubject_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();

            //Act
            bool result = mail.trySetSubject();

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void trySetSubject_EmptySubject_ReturnsFalse()
        {
            //Arange
            Mail mail = new Mail();
            string subject = "";

            //Act
            bool result = mail.trySetSubject(subject);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void trySetSubject_NotEmptySubject_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();
            string subject = "Your Energy Report";

            //Act
            bool result = mail.trySetSubject(subject);

            //Assert
            Assert.IsTrue(result);
        }

    }

    [TestClass]
    public class MailTests2
    {
        [TestMethod]
        public void trySetBody_DefaultBody_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();

            //Act
            bool result = mail.trySetBody();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void trySetBody_EmptyBody_ReturnsFalse()
        {
            //Arange
            Mail mail = new Mail();
            string body = "";

            //Act
            bool result = mail.trySetBody(body);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void trySetBody_NotEmptyBody_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();
            string body = "Atttached to this e-mail is your report.";

            //Act
            bool result = mail.trySetBody(body);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void tryAddAtachement_CorrectPath_ReturnsTrue()
        {
            //Arange
            Mail mail = new Mail();
            string path = @"C:\Users\shank\Pictures\Screenshots\Iteration1Picture1- PASS.png";

            //Act
            bool result = mail.tryAddAtachement(path);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void tryAddAtachement_WrongPath_ReturnsFalse()
        {
            //Arange
            Mail mail = new Mail();
            string path = "i am a cool path";

            //Act
            bool result = mail.tryAddAtachement(path);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
