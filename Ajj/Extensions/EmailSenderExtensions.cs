using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Models.AccountViewModels;
using Ajj.Service;
using Microsoft.AspNetCore.Http;
using MTIC.Service.Email;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Ajj.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Congratulation! you have successfully registerd with Ajj, Soon our team will contact your.<br/> Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>{HtmlEncoder.Default.Encode(link).ToString()}</a>");
        }

        public static Task SendEmailConfirmUserAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Congratulation! you have successfully registerd with Ajj, Please click on link confirmation : <a href='{HtmlEncoder.Default.Encode(link)}'>{HtmlEncoder.Default.Encode(link).ToString()}</a>");
        }

        public static Task SendEmailAdminAsync(this IEmailSender emailSender, string email, string userEmail)
        {
            return emailSender.SendEmailAjjAsync(email, "New Registration Ajj",
                $"Congratulation! New user has been registerd with email ID '{userEmail}'");
        }

        public static Task SendEmailWelcomeAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Welcome to Ajj",
                $"Congratulation! you have successfully registerd with Ajj, Soon our team will contact your");
        }

        public static Task SendEmailForJobAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Successfully apply for job",
                $"Congratulation! You have successfully registered for this job, We will contact you soon");
        }

        public static Task SendEmailAsync(this IEmailSender emailSender, string email, string subject, string body)
        {
            return emailSender.SendEmailAsync(email, subject,
                body);
        }
    }

    public static class EmailServiceExtensions
    {
        public static void SendEmailConfirmationAsync(this IEmailService emailService, string email, string candidatename, string link)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = "Thank You for your registration",
                Content = $"Dear {candidatename}, <br/> Thank you for your registration with us.<br/><br/> kindly note that registration is not finish yet<br/> Please click on this URL to complete registration.<br/>以下URLをクリックして登録を完了させてください<br/><br/> <a href='{HtmlEncoder.Default.Encode(link)}'>{HtmlEncoder.Default.Encode(link)}</a><br/><br/>株式会社オールジョブスジャパン<br/>All Jobs Japan<br/>URL : <a href='http://www.jobsjapan.net'>http://www.jobsjapan.net</a>"
            };
            emailService.Send(emailMessage);
            //End email code
        }

        /// <summary>
        /// This email sent when client register by himself
        /// </summary>
        /// <param name="emailService"></param>
        /// <param name="email"></param>
        /// <param name="link"></param>
        /// <param name="domainName"></param>
        /// <param name="model"></param>
        public static void SendEmailConfirmationClientAsync(this IEmailService emailService, string email, string link, string domainName, RegisterClientViewModel model)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            var companyName = model.CompanyName;
            var loginUrl = domainName + "/Account/LoginClient";

            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = "メールアドレス認証のお願い",
                Content = $"{companyName},<br/><br/>オールジョブスジャパン、企業アカウントへの登録ありがとうございます。本登録はまだ完了しておりません。次のURLをクリックし、メールアドレスを認証し、本登録を完了して下さい。<br/><br/><a href = '{HtmlEncoder.Default.Encode(link)}'> {HtmlEncoder.Default.Encode(link)} </a><br/><br/> 完了後、企業ログインページよりログインが可能になります。<br/> ログイン URL<br/> <br/><a href = 'http://www.{HtmlEncoder.Default.Encode(loginUrl)}' > http://www.{HtmlEncoder.Default.Encode(loginUrl)}</a><br/><br/>ログイン後、人材の募集条件をご入力下さい。<br/><br/>今後貴社の人材募集に携われることを楽しみにしております<br/><br/><table><tr><td colspan = '2'></tr><tr><td> 企業名 : </td><td> {model.CompanyName} </td></tr><tr><td> 企業URL :</td><td> {model.WebsiteUrl}</td></tr><tr><td> 担当者名 :</td><td> {model.ContactPerson} </td></tr><tr><td> 連絡用Email / ユーザー名 :</td><td> {model.CompanyEmail} </td></tr></table><br/><br/><br/>株式会社オールジョブスジャパン<br/>All Jobs Japan<br/>URL : <a href = 'http://www.jobsjapan.net'>http://www.jobsjapan.net</a><br/>"
            };
            emailService.Send(emailMessage);
            //End email code
        }

        //This email send when ajj admin register client
        public static void SendEmailRegisteredClientAsync(this IEmailService emailService, string email, string link, string domainName, Client model)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            var companyName = model.CompanyName;
            var loginUrl = domainName + "/Account/LoginClient";

            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = "メールアドレス認証のお願い",
                Content = $"{companyName}<br/>{model.ContactPerson} 様<br/><br/>オールジョブスジャパン、企業アカウントへの登録ありがとうございます。本登録はまだ完了しておりません。次のURLをクリックし、メールアドレスを認証し、本登録を完了して下さい。<br/><br/><a href = '{HtmlEncoder.Default.Encode(link)}'> {HtmlEncoder.Default.Encode(link)} </a><br/><br/> 完了後、企業ログインページよりログインが可能になります。<br/> ログイン URL<br/> <br/><a href = 'http://www.{HtmlEncoder.Default.Encode(loginUrl)}' > www.{HtmlEncoder.Default.Encode(loginUrl)}</a><br/><br/>ログイン後、人材の募集条件をご入力下さい。<br/><br/>今後貴社の人材募集に携われることを楽しみにしております<br/><br/><table><tr><td colspan = '2'></tr><tr><td> 企業名 : </td><td> {model.CompanyName} </td></tr><tr><td> 企業URL :</td><td> {model.WebsiteUrl}</td></tr><tr><td> 担当者名 :</td><td> {model.ContactPerson} </td></tr><tr><td> 連絡用Email / ユーザー名 :</td><td> {model.ContactEmail} </td></tr></table><br/><br/><br/>株式会社オールジョブスジャパン<br/>All Jobs Japan<br/>URL : <a href = 'http://www.{HtmlEncoder.Default.Encode(domainName)}'>www.{domainName}</a><br/>"
            };
            emailService.Send(emailMessage);
            //End email code
        }

        public static void SendEmailNewCompanyMemberAsync(this IEmailService emailService, List<EmailAddress> emails, string link,string domainName, string contactPerson, Client company)
        {
            
            var loginUrl = domainName + "/Account/LoginClient";

            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = emails,
                Subject = "メールアドレス認証のお願い",
                Content = $"{company.CompanyName}<br/>{contactPerson} 様<br/><br/>オールジョブスジャパン、企業アカウントへの登録ありがとうございます。本登録はまだ完了しておりません。次のURLをクリックし、メールアドレスを認証し、本登録を完了して下さい。<br/><br/><a href = '{HtmlEncoder.Default.Encode(link)}'> {HtmlEncoder.Default.Encode(link)} </a><br/><br/> 完了後、企業ログインページよりログインが可能になります。<br/> ログイン URL<br/> <br/><a href = 'http://www.{HtmlEncoder.Default.Encode(loginUrl)}' > www.{HtmlEncoder.Default.Encode(loginUrl)}</a><br/><br/>ログイン後、人材の募集条件をご入力下さい。<br/><br/>今後貴社の人材募集に携われることを楽しみにしております<br/><br/><table><tr><td colspan = '2'></tr><tr><td> 企業名 : </td><td> {company.CompanyName} </td></tr><tr><td> 企業URL :</td><td> {company.WebsiteUrl}</td></tr><tr><td> 担当者名 :</td><td> {company.ContactPerson} </td></tr><tr><td> 連絡用Email / ユーザー名 :</td><td> {company.ContactEmail} </td></tr></table><br/><br/><br/>株式会社オールジョブスジャパン<br/>All Jobs Japan<br/>URL : <a href = 'http://www.{HtmlEncoder.Default.Encode(domainName)}'>www.{domainName}</a><br/>"
            };
            emailService.Send(emailMessage);
            //End email code
        }

        public static void SendEmailAdminAsync(this IEmailService emailService, string email, string userEmail, string userType = null)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            string subject = "";
            string content = "";

            if (userType == "client")
            {
                subject = "New Client Registered";
                content = $"Congratulation! New client has been registerd with email ID '{userEmail}' on {DateTime.Now.ToShortDateString()}";
            }
            else
            {
                subject = "New User Registered";
                content = $"Congratulation! New user has been registerd with email ID '{userEmail}' on {DateTime.Now.ToShortDateString()}";
            }
            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = subject,
                Content = content
            };
            emailService.Send(emailMessage);
            //End email code
        }

        public static void SendEmailGaijinAsync(this IEmailService emailService, string email, string userEmail)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            string subject = "";
            string content = "";

            subject = "New GB User Registered";
            content = $"Congratulation! Ajj user registered on GB side with email ID '{userEmail}' on {DateTime.Now.ToShortDateString()}";


            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = subject,
                Content = content
            };
            emailService.Send(emailMessage);
            //End email code
        }

        public static void SendErrorEmailAsync(this IEmailService emailService, string email, string error, string apiType,string userEmail)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = "Error in AJJ",
                Content = $"Error occured while sync with API '{error}' in {apiType} API on {DateTime.Now.ToShortDateString()} for user email : {userEmail}"
            };
            emailService.Send(emailMessage);
            //End email code
        }
        public static void SendSpecificMailAsync(this IEmailService emailService, string email)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = "Please Register Again",
                Content = $"Sorry to you inform you, but because of some mantinence work we could't receive you registration information, Please register again for getting exciting job offers, Sorry for Inconvenience.<br/>Please click on below link for registration :<br/>http://jobsjapan.net/Account/Register<br/><br/>" +
                $"All Jobs Japan"
            };
            emailService.Send(emailMessage);
            //End email code
        }

        public static async Task SendEmailAsync(this IEmailService emailService, string email, string subject, string body)
        {
            //Email sending code
            EmailAddress toEmailAddress = new EmailAddress();
            toEmailAddress.Name = email;
            toEmailAddress.Address = email;
            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = subject,
                Content = body
            };
            await emailService.SendAsync(emailMessage);
            //End email code
        }

        /// <summary>
        /// By Default mail is send by no-reply-notification@mtic.co.jp
        /// </summary>
        /// <param name="emailService"></param>
        /// <param name="emails">List of reciever emails</param>
        /// <param name="key">key for email format</param>
        /// <returns></returns>
        public static bool SendEmailAsync2(this IEmailService emailService, List<EmailAddress> emails, string key, string jobTitle, string companyName, string candidateName)
        {
            dynamic emailFormats =
                   JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Formats/emails.json"));
            var emailformat = emailFormats[key];

            if (emailformat != null)
            {
                //var fromemails = emailformat["fromemail"];

                //if(fromemails != null && fromemails != "")
                //{
                //    List<EmailAddress> fromEmails = new List<EmailAddress>()
                //    {
                //        new EmailAddress()
                //        {
                //            Name = emailformat["fromemail"],
                //            Address = emailformat["fromename"]
                //        }
                //    };
                //}
                string mailContent = emailformat["content"].Value;
                mailContent = mailContent.Replace("$jobtitle$", jobTitle).Replace("$companyname$", companyName).Replace("$candidatename$", candidateName);

                EmailMessage emailMessage = new EmailMessage()
                {
                    ToAddresses = emails,
                    Subject = emailformat["subject"].Value,
                    Content = mailContent
                };

                emailService.Send(emailMessage);
                return false;
            }
            else
            {
                return true;
            }

            //End email code
        }

        public static bool SendEmailConfirmationAsync2(this IEmailService emailService, List<EmailAddress> emails, string key, string callbackurl)
        {
            dynamic emailFormats =
                   JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Formats/emails.json"));
            var emailformat = emailFormats[key];

            if (emailformat != null)
            {
                EmailMessage emailMessage = new EmailMessage()
                {
                    ToAddresses = emails,
                    Subject = emailformat["subject"].Value,
                    Content = emailformat["content"].Value
                };

                emailService.Send(emailMessage);
                return false;
            }
            else
            {
                return true;
            }

            //End email code
        }
    }
}