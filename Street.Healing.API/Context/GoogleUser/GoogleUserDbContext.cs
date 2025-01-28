using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Street.Healing.API.Context.GoogleUser
{
    public class GoogleUserDbContext(DbContextOptions<GoogleUserDbContext> options) : IdentityDbContext<GoogleUser>(options)
    {
    }
}
