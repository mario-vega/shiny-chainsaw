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
						access_token = null,
						user_id = null,
						expires_in = 0,
						token_type = "bearer"
					});
				
				// Generate a token by user.
				var securityToken = _jwtService.GetJwtSecurityToken(user);
				// Serialize the token into JWT format.
				var jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);
				var expires_sec = Convert.ToInt32(_configuration["JwtSettings:ExpirationTimeInMinutes"]) * 60);

				var token = new LoginToken
				{
					access_token = jwt,
					expires_in = expires_sec,
					user_id = user.Id.ToString(),
					token_type = "Bearer"
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
