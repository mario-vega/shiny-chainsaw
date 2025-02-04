namespace Shiny.Chainsaw.DTO
{
	public class Payment
	{
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdCustomer { get; set; }
        public int IdUser { get; set; }
        public decimal Amount { get; set; }

    }
}
