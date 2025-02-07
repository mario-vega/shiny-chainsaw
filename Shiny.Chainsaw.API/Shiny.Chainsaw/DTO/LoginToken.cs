namespace Shiny.Chainsaw.DTO
{
	public class LoginToken
	{
		/// <summary>
		/// The JWT token if the login attempt is successful, or NULL if not
		/// </summary>
		public string? accessToken { get; set; }
		public string? userId { get; set; }
		public string? tokenType { get; set; }
		/// <summary>
		/// expires time in seconds
		/// </summary>
		/// <value></value>
		public int? expiresIn { get; set; }
		public int? refreshToken { get; set; }
	}
}
