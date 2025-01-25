using Microsoft.Extensions.Primitives;

namespace Street.Healing.API.Helpers
{
    public static class ErrorMessages
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




    }
}
