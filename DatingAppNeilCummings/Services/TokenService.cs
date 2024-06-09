using DatingAppNeilCummings.Entities;
using DotNetCoreIdentity.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingAppNeilCummings.Services
{
    public class TokenService : ITokenService
    {
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;
		public TokenService(IConfiguration config)
		{
			_config = config;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
		}

		public string CreateToken(AppUser user)
		{
			var claims = new List<Claim>{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.Email)
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

			var securityDes = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				//Issuer = _config["Token:Issuer"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(securityDes);

			return tokenHandler.WriteToken(token);


		}
	}
}