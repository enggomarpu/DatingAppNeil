using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppNeilCummings.Controllers
{
	
	public class UsersController : BaseApiController
	{
		private readonly DBContext _context;

		public UsersController(DBContext context)
		{
			_context = context;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		//[Authorize]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}
	}
}
