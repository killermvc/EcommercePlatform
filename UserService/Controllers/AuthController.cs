using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;
using UserService.ViewModels;

namespace UserService.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IConfiguration _configuration;

	public AuthController(
		UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		IConfiguration configuration)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
	{
		var user = new ApplicationUser
		{
			UserName = model.Email,
			Email = model.Email,
			FullName = model.FullName
		};

		var result = await _userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
			return BadRequest(result.Errors);

		return Ok(new { Message = "User created successfully" });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginViewModel model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
			return Unauthorized(new { Message = "Invalid credentials" });

		var token = GenerateJwtToken(user);
		return Ok(new { Token = token });
	}

	private string GenerateJwtToken(ApplicationUser user)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"]));

		var token = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			claims,
			expires: expires,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
