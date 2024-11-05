using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface ICheckinRepository
	{
		public Task<int> Add(Checkin history);
		public Task<IEnumerable<Checkin>> GetByCustomer(int id);
	}
}
