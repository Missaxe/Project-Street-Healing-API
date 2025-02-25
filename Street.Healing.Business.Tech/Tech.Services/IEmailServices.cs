using Street.Healing.Business.Tech.Helpers;
using System.Threading.Tasks;

namespace Street.Healing.Business.Tech.Tech.Services
{
    public interface IEmailServices
    {
        public Task SendEmailAsync(Message message);

    }
}
