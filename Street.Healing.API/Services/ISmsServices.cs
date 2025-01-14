using Twilio.Rest.Api.V2010.Account;

namespace Street.Healing.API.Services
{
    public interface ISmsServices
    {
        public MessageResource SendSms();
    }
}
