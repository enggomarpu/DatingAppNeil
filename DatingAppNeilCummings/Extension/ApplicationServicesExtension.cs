using DatingAppNeilCummings.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DatingAppNeilCummings.Extension
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<DBContext>(x =>
            {
                x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddIdentityCore<AppUser>(opt => {
            //	opt.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<DBContext>();

            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            

            return services;
        }

    }
}