using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface ICheckinHistoryRepository
	{
		public void Add(CheckinHistory history);
		public Task<IEnumerable<CheckinHistory>> GetByCustomer(int id);
	}
}
