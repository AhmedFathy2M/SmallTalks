using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallTalks.Dtos;
using SmallTalks.Extensions;
using SmallTalks.Services;
using System.Security.Claims;

namespace SmallTalks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		/// <summary>
		/// used to check login credentials.
		/// </summary>
		/// <param name="userManager"> to receive injectted manager</param>
		/// <param name="signInManager"> to receive injectted manager</param>
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		[Authorize]
		[HttpGet]

		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var user = await _userManager.FindUserByEmailFromClaimsPrincipleAsync(User);
			if (user != null)
			{
				ChatService.AddUserToList(user.DisplayName);
				return new UserDto()
				{
					Id = user.Id,
					Email = user.Email,
					Token = _tokenService.CreateToken(user),
					DisplayName = user.DisplayName
				};

			}
			else
			{
				return BadRequest("No user found");
			}

		}
		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
		{
			return await _userManager.FindByEmailAsync(email) != null;
		}
		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{

			var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(User);
			return new AddressDto()
			{
				FirstName = user.Address.FirstName,
				LastName = user.Address.LastName,
				City = user.Address.City,
				State = user.Address.State,
				ZipCode = user.Address.ZipCode,
				Street = user.Address.Street
			};
		}
		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
		{
			var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(User);
			user.Address.FirstName = address.FirstName;
			user.Address.LastName = address.LastName;
			user.Address.Street = address.Street;
			user.Address.City = address.City;
			user.Address.State = address.State;
			user.Address.ZipCode = address.ZipCode;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return Ok(new AddressDto()
				{
					City = user.Address.City,
					State = user.Address.State,
					ZipCode = user.Address.ZipCode,
					Street = user.Address.Street,
					FirstName = user.Address.FirstName,
					LastName = user.Address.LastName

				});
			}
			else
			{
				return BadRequest("Problem updating user");
			}
		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				return Unauthorized("User not found");
			}
			else
			{
				var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
				if (!result.Succeeded)
				{
					return Unauthorized("Failed login attempt, please check credentials and try again");
				}
				else
				{
					ChatService.AddUserToList(user.DisplayName);
					return new UserDto()
					{
						Email = loginDto.Email,
						Token = _tokenService.CreateToken(user),
						DisplayName = user.DisplayName
					};
				}

			}

		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
			{
				return BadRequest("Email address is already in use");
			}
			var user = new AppUser()
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.Email
			};
			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(error => error.Description).ToList();
				return BadRequest(errors);
			}
			else
			{
				ChatService.AddUserToList(user.DisplayName);
				return new UserDto()
				{
					DisplayName = user.DisplayName,
					Email = user.Email,
					Token = _tokenService.CreateToken(user)
				};
			}
		}

		[HttpGet("usernames")]
		public async Task<ActionResult<List<string>>> GetAllNamesOfUsersAsync()
		{
			var userNames = await _userManager.GetAllUserNamesAsync();
			return Ok(userNames.ToList());
		}

		[HttpGet("allusers")]
		public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();
			var userDtos = users.Select(user => new UserDto
			{
				Email = user.Email,
				DisplayName = user.UserName

			}).ToList();

			return Ok(userDtos);
		}



	}
}
