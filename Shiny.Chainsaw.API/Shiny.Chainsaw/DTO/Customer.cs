namespace Shiny.Chainsaw.DTO
{
	public class Customer
	{
        public int Id { get; set; }
        public int Name { get; set; }
        public required string Telephone { get; set; }
        public required string TelephoneEmergency { get; set; }
	}
}
