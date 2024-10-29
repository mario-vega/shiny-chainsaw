using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface ICustomerRepository
	{
		public void Add(Customer customer);
		public void Update(Customer customer);
		public Task<Customer> Get(int id);
		public Task<IEnumerable<Customer>> Get();
	}
}
