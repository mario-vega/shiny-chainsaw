using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository.DbContext;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Shiny.Chainsaw.Repository
{
	public class UserRepository : IUserRepository
	{
		IDapperContext _dbcontext;
		public UserRepository(IDapperContext dbcontext) 
		{
			_dbcontext = dbcontext;
		}

		public async Task<int> Add(User user)
		{
			var query = @"INSERT INTO [shiny-chainsaw].[dbo].[users] (FirstName, LastName, Telephone, Address, EMail, Admin, Username, Password)
							OUTPUT INSERTED.Id
							VALUES(@FirstName, @LastName, @Telephone, @Address, @EMail, @Admin, @Username, @Password);";

			var parameters = new DynamicParameters();
			parameters.Add("FirstName", user.FirstName, DbType.String);
			parameters.Add("LastName", user.LastName, DbType.String);
			parameters.Add("Telephone", user.Telephone, DbType.String);
			parameters.Add("Address", user.Address, DbType.String);
			parameters.Add("EMail", user.EMail, DbType.String);
			parameters.Add("Admin", user.Admin, DbType.String);
			parameters.Add("Username", user.Username, DbType.String);
			parameters.Add("Password", user.Password, DbType.String);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				return await db.QuerySingleAsync<int>(query, parameters);
			}
		}

		public async Task<User> Get(int id)
		{
			User response;
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryFirstAsync<User>(sql: @"SELECT Id, FirstName, LastName, Telephone, Address, EMail, Admin, Username, Password
							FROM [shiny-chainsaw].[dbo].[users] WHERE [Id] = @Id", parameters, commandType: CommandType.Text);
			}
			return response;
		}

		public async Task<User> Get(LoginRequest loginRequest)
		{
			User response;
			var parameters = new DynamicParameters();
			parameters.Add("Username", loginRequest.Username, DbType.String);
			parameters.Add("Password", loginRequest.Password, DbType.String);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryFirstAsync<User>(sql: @"SELECT Id, FirstName, LastName, Telephone, Address, EMail, Admin, Username, Password
							FROM [shiny-chainsaw].[dbo].[users] WHERE [Username] = @Username AND [Password] = @Password", parameters, commandType: CommandType.Text);
			}
			return response;
		}


		public async Task<IEnumerable<User>> Get()
		{
			IEnumerable<User> response;
			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryAsync<User>(sql: @"SELECT Id, FirstName, LastName, Telephone, Address, EMail, Admin, Username, Password
							FROM [shiny-chainsaw].[dbo].[users]", commandType: CommandType.Text);
			}
			return response;
		}

		public async Task Update(User user)
		{
			var query = @"UPDATE [shiny-chainsaw].[dbo].[users] SET FirstName=@FirstName, LastName=@LastName, Telephone=@Telephone, Address=@Address, EMail=@EMail, Admin=@Admin, Username=@Username, Password=@Password
						WHERE Id=@Id;";

			var parameters = new DynamicParameters();
			parameters.Add("Id", user.Id, DbType.Int16);
			parameters.Add("FirstName", user.FirstName, DbType.String);
			parameters.Add("LastName", user.LastName, DbType.String);
			parameters.Add("Telephone", user.Telephone, DbType.String);
			parameters.Add("Address", user.Address, DbType.String);
			parameters.Add("EMail", user.EMail, DbType.String);
			parameters.Add("Admin", user.Admin, DbType.String);
			parameters.Add("Username", user.Username, DbType.String);
			parameters.Add("Password", user.Password, DbType.String);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				await db.ExecuteAsync(query, parameters);
			}
		}
	}
}
