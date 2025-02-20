using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Street.Healing.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DAO.Context
{
    public class UserGoogleDbContext(DbContextOptions<UserGoogleDbContext> options) : IdentityDbContext<UserGoogle>(options)
    {
    }
}
