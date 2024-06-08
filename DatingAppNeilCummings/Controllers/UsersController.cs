using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppNeilCummings.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly DBContext _context;

		public UsersController(DBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}
	}
}
