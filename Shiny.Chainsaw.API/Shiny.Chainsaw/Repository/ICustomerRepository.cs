using Shiny.Chainsaw.DTO;

namespace Shiny.Chainsaw.Repository
{
	public interface ICustomerRepository
	{
		public Task<int> Add(Customer customer);
		public void Update(Customer customer);
		public Task<Customer> Get(int id);
		public Task<IEnumerable<Customer>> Get();
	}
}
