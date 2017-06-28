using System;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using TaskDomain.DomainModel;

namespace TaskManagerUtility
{
    public class EmailUtility
    {
        public void SendMail(UserDetailDm userDetail)
        {
            try
            {
                var receiver = userDetail.EmailId;
                var sender = ConfigurationManager.AppSettings["EmailSender"];
                var senderPassword = ConfigurationManager.AppSettings["Password"];
                EncryptionDecryption encrypter = new EncryptionDecryption();
                var encryptedReceiver = encrypter.Encrypt(receiver);

                var callbackUrl = new Uri("http://localhost:52914/Email/Verify" + "?username=" + HttpContext.Current.Server.UrlEncode(encryptedReceiver));
                var str = HttpContext.Current.Request.QueryString["url"];
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                String body = "Welcome to Task Manager. <a href = " + callbackUrl.AbsoluteUri + ">Click here to Verify</a>";
                mail.From = new MailAddress(sender);
                mail.To.Add(receiver);
                mail.Subject = "Welcome To Task Manager";
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential(sender, senderPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch
            {
                throw;
            }
        }
    }
}
