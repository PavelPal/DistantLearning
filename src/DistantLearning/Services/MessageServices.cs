using System.Threading.Tasks;

namespace DistantLearning.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // todo email sender
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // todo sms sender
            return Task.FromResult(0);
        }
    }
}