using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Ajj.Service
{
    public class AuthEmailSender : IEmailSender
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //public bool SendSmtpMail()
        //{
        //    bool status = false;
        //    try
        //    {
        //        SmtpClient client = new SmtpClient("smtp.gmail.com");
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(Username, Password);
        //        client.Port = 587;// 587;
        //        MailMessage mailMessage = new MailMessage();
        //        mailMessage.From = new MailAddress(From);
        //        mailMessage.To.Add(To);
        //        mailMessage.Body = Body;
        //        mailMessage.Subject = Subject;
        //        client.Send(mailMessage);
        //        status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        status = false;
        //    }

        //    return status;
        //}

        public async Task SendEmailAsync(string email, string subjectText, string message)
        {
            try
            {
                //From Address
                string fromAddress = "no-reply-notification@mtic.co.jp";
                string fromAdressTitle = "Gaijin Bank";
                //To Address
                string toAddress = email;
                string toAdressTitle = "GaijinBank";
                string subject = subjectText;
                string bodyContent = message;

                //Smtp Server
                string smtpServer = "mtic-server.sakura.ne.jp";
                //Smtp Port Number
                int smtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (fromAdressTitle,
                                         fromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (toAdressTitle,
                                         toAddress
                                         ));
                mimeMessage.Subject = subject; //Subject
                mimeMessage.Body = new TextPart("html")
                {
                    Text = bodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(smtpServer, smtpPortNumber, SecureSocketOptions.Auto);
                    //client.Authenticate(
                    //    "dhananjay.singh011@gmail.com",
                    //    "Et1f1lav"
                    //    );

                    //client.Connect(SmtpServer, SmtpPortNumber, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(
                        "no-reply-notification@mtic.co.jp",
                        "mtic0121"
                        );

                    // client.ServerCertificateValidationCallback = System.Net.Security.RemoteCertificateValidationCallback;
                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendEmailAjjAsync(string email, string subjectText, string message)
        {
            try
            {
                //From Address
                string fromAddress = "no-reply-notification@mtic.co.jp";
                string fromAdressTitle = "Gaijin Bank";
                //To Address
                string toAddress = email;
                string toAdressTitle = "GaijinBank";
                string subject = subjectText;
                string bodyContent = message;

                //Smtp Server
                string smtpServer = "mtic-server.sakura.ne.jp";
                //Smtp Port Number
                int smtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (fromAdressTitle,
                                         fromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (toAdressTitle,
                                         toAddress
                                         ));
                mimeMessage.Subject = subject; //Subject
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = bodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(smtpServer, smtpPortNumber, SecureSocketOptions.Auto);
                    //client.Connect(SmtpServer, SmtpPortNumber, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(
                        "no-reply-notification@mtic.co.jp",
                        "mtic0121"
                        );

                    // client.ServerCertificateValidationCallback = System.Net.Security.RemoteCertificateValidationCallback;
                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}