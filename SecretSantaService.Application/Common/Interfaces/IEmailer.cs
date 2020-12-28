using System.Threading.Tasks;

namespace SecretSantaService.Application.Common.Interfaces
{
    public interface IEmailer
    {
        Task Send(string to, string subject, string body, bool isHtmlBody = true);
    }
}
