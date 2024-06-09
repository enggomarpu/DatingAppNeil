using AppIdentity.Dtos;
using DatingAppNeilCummings.Entities;
using DotNetCoreIdentity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppNeilCummings.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _siginManger;
		 private readonly ITokenService _tokenService;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> siginManger,   ITokenService tokenService)
		{
			_userManager = userManager;
			_siginManger = siginManger;
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto regisDto)
		{
			if (await UserExists(regisDto.Email)) return BadRequest("User already taken");

			var user = new AppUser
			{
				Email = regisDto.Email,
				FirstName = regisDto.FirstName,
				LastName = regisDto.LastName,
				UserName = regisDto.Email
			};

			var result = await _userManager.CreateAsync(user, regisDto.Password);
			if (!result.Succeeded)
			{
				return BadRequest();
			}

			return new UserDto
			{
				Email = regisDto.Email,
				Token =  _tokenService.CreateToken(user),
				UserName = regisDto.Email
			};

		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{

			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				return Unauthorized();
			}

			var result = await _siginManger.CheckPasswordSignInAsync(user, loginDto.Password, true);

			if (result.IsLockedOut)
			{
				return Unauthorized("User is locked out");
			}

			if (!result.Succeeded)
			{
				return Unauthorized();
			}
			return new UserDto
			{
				Email = loginDto.Email,
				UserName = loginDto.Email,
				Token = _tokenService.CreateToken(user)
			};
		}




		private async Task<bool> UserExists(string username)
		{
			return await _userManager.Users.AnyAsync(x => x.Email == username);
		}





	}
}