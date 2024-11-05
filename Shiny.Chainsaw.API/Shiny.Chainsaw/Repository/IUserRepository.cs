using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface IUserRepository
	{
		public Task<int> Add(User user);
		public void Update(User user);
		public Task<User> Get(int id);
		public Task<IEnumerable<User>> Get();
	}
}
