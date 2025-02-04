using Dapper;
using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository.DbContext;
using System.Data;

namespace Shiny.Chainsaw.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly IDapperContext _dbcontext;

        public CustomerRepository(IDapperContext context)
        {
			_dbcontext = context;    
        }

        public async Task<int> Add(Customer customer)
		{
			var query = @"INSERT INTO [dbo].[customers] ([Name], [Telephone], [TelephoneEmergency]) OUTPUT INSERTED.Id VALUES (@Name, @Telephone, @TelephoneEmergency);";

			var parameters = new DynamicParameters();
			parameters.Add("Name", customer.Name, DbType.String);
			parameters.Add("Telephone", customer.Telephone, DbType.String);
			parameters.Add("TelephoneEmergency", customer.TelephoneEmergency, DbType.String);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				return await db.QuerySingleAsync(query, parameters);
			}
		}

		public async Task<Customer> Get(int id)
		{
			Customer response;

			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryFirstAsync<Customer>(sql: @"SELECT [Id], [Name], [Telephone], [TelephoneEmergency] FROM [dbo].[customers] WHERE [Id] = @Id", commandType: CommandType.Text, param: parameters);
			}
			return response;
		}

		public async Task<IEnumerable<Customer>> Get()
		{
			IEnumerable<Customer> response;
			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryAsync<Customer>(sql: @"SELECT [Id], [Name], [Telephone], [TelephoneEmergency] FROM [dbo].[customers]", commandType: CommandType.Text);
			}
			return response;
		}

		public async void Update(Customer customer)
		{
			var query = @"UPDATE [dbo].[customers] SET [Name] = @Name, [Telephone] = @Telephone, [TelephoneEmergency] = @TelephoneEmergency WHERE [Id] = @Id;";

			var parameters = new DynamicParameters();
			parameters.Add("Name", customer.Name, DbType.String);
			parameters.Add("Telephone", customer.Telephone, DbType.String);
			parameters.Add("TelephoneEmergency", customer.TelephoneEmergency, DbType.String);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				await db.ExecuteAsync(query, parameters);
			}
		}
	}
}
