namespace Street.Healing.API.RequestsDto.GoogleSignIn
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful;
        public string? ErrorMessage;
        public string? Token;
        public bool Is2StepVerificationRequired;
        public string? Provider;
    }
}
