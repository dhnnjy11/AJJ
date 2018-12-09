using Ajj.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        Task SendAsync(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}