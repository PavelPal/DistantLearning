using System.Threading.Tasks;

namespace DistantLearning.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}