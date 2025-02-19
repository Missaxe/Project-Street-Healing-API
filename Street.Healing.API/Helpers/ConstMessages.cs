using Microsoft.Extensions.Primitives;
using Twilio.Rest.Marketplace.V1;

namespace Street.Healing.API.Helpers
{
    public static class ConstMessages
    {
        public const string EmailAlreadyExists = "This email already exists. Please try with another one.";
        public const string EmailSent = "Email successfully sent.";
        public const string EmptyUserObj = "User is Empty , No user to be registred.";
        public const string UserNotFound = "User not found.";
        public const string UserNotAdded = "User not added";
        public const string UserAdded = "User successfully added.";
        public const string UserLogged = "User successfully logged.";
        public const string PasswordsNotTheSame = "Passwords do not match. Please try again.";
        public const string IncorrectPasword = "Password is incorrect. Please try again.";
        public const string EmptyPasword = "Password is empty. Please try again.";
        public const string OtpFailed = "Failed to send OTP email.";
        public const string GoogleSignInFailed = "Failed to sign in with google.";
        public const string ApiKeyNotProvided = "Api Key was not provided.";
        public const string ApiKeyKO= "problem with Api Key , check logs for more details .";
        public const string RateLimitKO = "problem with RateLimiting , check logs for more details .";
        public const string UnauthorizedClient= "Unauthorized client.";
        public const string InvalidAuthentication = "Invalid External Authentication.";
        public const string ExInvkAsyncJWT = "Exception during the process InvokeAsync in JWTTokenMiddleware :";
        public const string ExInvkMiddlewaFct = "Exception during the process InvokeAsync in MiddlewareFct :";
        public const string ExceptionExternalLogin = "Exception during the process of adding the user with Google Account in GoogleSignInController :";
        public const string ExInvkAsyncRateLimit = "Exception during the process InvokeAsync in ExternalLogin :";
        public const string ExceptionSignUpUser = "Exception during the process of adding user in AddUserAsync :";
        public const string ExceptionIsValidEmail= "Exception in method IsValidEmail from class DataValidators :";
        public const string ExceptionVerifyPass = "Exception in method VerifyPassword from class DataValidators :";
        public const string ExceptionLoginUser = "Exception during the process of login user in Authenticate :";
        public const string ExceptionVerifyToken = "Exception during the process of verifying google token :";
        public const string ExceptionSendToken = "Exception during the process of sending token in OtpController :";
        public const string InvalidExterAuthentication = "Invalid External Authentication.";
        public const string OtpConfirmation = "OTP Confirmation";
        public const string MinLenghtPass = "Minimum password length should be 8";
        public const string AlphaNumPass = "Password should be AlphaNumeric";
        public const string RegexPass = "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]";
        public const string SpecialCharPass =  "Password should contain special character";
        public const string XApiKey = "XApiKey";
        public const string RateLimitExceeded = "Rate limit exceeded";
 



    }
}
