using System.Threading.Tasks;

namespace DistantLearning.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}