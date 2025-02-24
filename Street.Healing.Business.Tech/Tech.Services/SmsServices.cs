using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Street.Healing.API.Services
{
    public class SmsServices : ISmsServices
    {
        //public MessageResource SendSms()
        //{
        //    string sId = "AC329e3a5c9836d5822527ebb1dd5856fe"; // add Account from Twilio
        //    string authToken = "7b5356b9fe6de8f7302969f84c25f8a3"; //add Auth Token from Twilio
        //    string fromPhoneNumber = "+12292107604"; //add Twilio phone number

        //    TwilioClient.Init(sId, authToken);
        //    var message = MessageResource.Create(
        //        body: "Hi, there!!",
        //        from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
        //        to: new Twilio.Types.PhoneNumber("+765259316") //add receiver's phone number
        //    );
        //    return message;
        //}
    }
}
