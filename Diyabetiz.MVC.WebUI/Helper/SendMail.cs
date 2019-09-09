using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Diyabetiz.MVC.WebUI.Helper
{
    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort =587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }

    #region Metod1
    //public class SendMail
    //{
    //    public static string mailAlert;
    //    public static void SendEmail(string toAddress, string fromAddress, string subject, string message)                        
    //    {
    //        try
    //        {
    //            using (var mail = new MailMessage())
    //            {
    //                const string email = "username@yahoo.com";
    //                const string password = "password!";

    //                var loginInfo = new NetworkCredential(email, password);


    //                mail.From = new MailAddress(fromAddress);
    //                mail.To.Add(new MailAddress(toAddress));
    //                mail.Subject = subject;
    //                mail.Body = message;
    //                mail.IsBodyHtml = true;

    //                try
    //                {
    //                    using (var smtpClient = new SmtpClient("smtp.mail.yahoo.com", 465))
    //                    {
    //                        smtpClient.EnableSsl = true;
    //                        smtpClient.UseDefaultCredentials = false;
    //                        smtpClient.Credentials = loginInfo;
    //                        smtpClient.Send(mail);
    //                    }

    //                }

    //                finally
    //                {
    //                    //dispose the client
    //                    mail.Dispose();
    //                }

    //            }
    //        }   
    //        catch (SmtpFailedRecipientsException ex)
    //        {
    //            foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
    //            {
    //                var status = t.StatusCode;
    //                if (status == SmtpStatusCode.MailboxBusy ||
    //                    status == SmtpStatusCode.MailboxUnavailable)
    //                {
    //                    mailAlert="Delivery failed - retrying in 5 seconds.";
    //                    Thread.Sleep(5000);

    //                    //resend
    //                    //smtpClient.Send(message);
    //                }
    //                else
    //                {
    //                    mailAlert = String.Format("Failed to deliver message to {0}", t.FailedRecipient);


    //                }
    //            }
    //        }
    //        catch (SmtpException Se)
    //        {
    //            // handle exception here
    //            mailAlert = Se.ToString();
    //        }
    //        catch (Exception ex)
    //        {
    //            mailAlert = ex.ToString();
    //        }

    //    }
    //}
    #endregion

}