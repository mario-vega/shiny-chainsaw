using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface IPaymentRepository
	{
		public void Add(Payment paymentHistory);
		public void Remove(Payment paymentHistory);
		public Task<IEnumerable<Payment>> GetMonthly(DateTime refDate);
	}
}
