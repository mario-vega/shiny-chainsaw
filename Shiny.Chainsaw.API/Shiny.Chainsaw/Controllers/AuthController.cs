using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository;
using Shiny.Chainsaw.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Shiny.Chainsaw.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly JwtService _jwtService;
		private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, JwtService jwtService, IConfiguration configuration)
        {
            _userRepository = userRepository;
			_jwtService = jwtService;
			_configuration = configuration;
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			try
			{
				if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
					return BadRequest();

				var user = await _userRepository.Get(loginRequest);

				if (user == null)
					return Unauthorized(new LoginToken
					{
						accessToken = null,
						userId = null,
						expiresIn = 0,
						tokenType = "bearer"
					});
				
				// Generate a token by user.
				var securityToken = _jwtService.GetJwtSecurityToken(user);
				// Serialize the token into JWT format.
				var jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);
				var expires_sec = Convert.ToInt32(_configuration["JwtSettings:ExpirationTimeInMinutes"]) * 60;

				var token = new LoginToken
				{
					accessToken = jwt,
					expiresIn = expires_sec,
					userId = user.Id.ToString(),
					tokenType = "Bearer"
				};

				return Ok(token);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

    }
}
