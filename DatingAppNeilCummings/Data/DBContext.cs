using DatingAppNeilCummings.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppNeilCummings.Data
{
	public class DBContext : DbContext
	{
        public DBContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
