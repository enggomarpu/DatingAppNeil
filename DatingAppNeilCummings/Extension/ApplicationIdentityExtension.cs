

using System.Text;
using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.Entities;
using DatingAppNeilCummings.Services;
using DotNetCoreIdentity.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DatingAppNeilCummings.Extension
{
    public static class ApplicationIdentityExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<DBContext>();

            var jwtKey = config.GetSection("Token:Key").Get<string>();
            var jwtIssuer = config.GetSection("Token:Issuer").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                //ValidIssuer = jwtIssuer,
                //IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateAudience = false

            };
        });


            services.AddTransient<ITokenService, TokenService>();


            return services;
        }

    }
}