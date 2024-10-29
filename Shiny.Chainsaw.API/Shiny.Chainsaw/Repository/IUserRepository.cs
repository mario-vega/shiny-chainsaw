using Shiny.Chainsaw.Model;

namespace Shiny.Chainsaw.Repository
{
	public interface IUserRepository
	{
		public void Add(User user);
		public void Update(User user);
		public User Get(int id);
		public Task<IEnumerable<User>> Get();
	}
}
