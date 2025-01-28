using Microsoft.AspNetCore.Identity;

namespace Street.Healing.API.Context.GoogleUser
{
    public class GoogleUser :IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
