using System.Threading.Tasks;

namespace Ajj.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAjjAsync(string email, string subject, string message);
    }
}