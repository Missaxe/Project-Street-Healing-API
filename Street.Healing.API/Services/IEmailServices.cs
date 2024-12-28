using Street.Healing.API.Helpers;
using System.Threading.Tasks;

namespace Street.Healing.API.Services
{
    public interface IEmailServices
    {
        public Task SendEmailAsync(Message message);

        public string CreateJwt();

        public bool IsValidEmail(string email);
    }
}
