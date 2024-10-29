using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface IPaymentHistoryRepository
	{
		public void Add(PaymentHistory paymentHistory);
		public void Remove(PaymentHistory paymentHistory);
		public Task<IEnumerable<PaymentHistory>> GetMonthly(DateTime refDate);
	}
}
