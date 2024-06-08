using DatingAppNeilCummings.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatingAppNeilCummings.Data
{
	public class DBContext : IdentityDbContext<AppUser>
	{
        public DBContext(DbContextOptions options) : base(options)
        {
            
        }
        
    }
}
