namespace Shiny.Chainsaw.Model
{
	public class User
	{
        public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
        public required string Telephone { get; set; }
        public required string Address { get; set; }
        public bool Admin { get; set; }
        public required string EMail { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
