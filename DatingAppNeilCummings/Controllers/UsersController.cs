using AutoMapper;
using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.DTOs;
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
		private readonly IMapper _mapper;

		public UsersController(DBContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		//[Authorize]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			
			var users = await _context.Users.Include(e => e.Photos).ToListAsync();
			var returnUsers = _mapper.Map<IEnumerable<MemberDto>>(users);

			return Ok(returnUsers);

		}
	}
}
