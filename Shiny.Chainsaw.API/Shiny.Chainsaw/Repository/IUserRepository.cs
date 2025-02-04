using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface IUserRepository
	{
		public Task<int> Add(User user);
		public Task Update(User user);
		public Task<User> Get(int id);
		public Task<User> Get(string username, string password);
		public Task<IEnumerable<User>> Get();
	}
}
