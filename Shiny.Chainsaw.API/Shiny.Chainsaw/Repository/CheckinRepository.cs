using Dapper;
using Shiny.Chainsaw.DTO;
using Shiny.Chainsaw.Repository.DbContext;
using System.Data;

namespace Shiny.Chainsaw.Repository
{
	public class CheckinRepository : ICheckinRepository
	{
		private readonly IDapperContext _dbcontext;

		public CheckinRepository(IDapperContext context)
		{
			_dbcontext = context;
		}

		public async Task<int> Add(Checkin checkin)
		{
			var query = @"INSERT INTO [dbo].[checkin_history] ([Date], [IdCustomer]) OUTPUT INSERTED.Id VALUES (@Date, @IdCustomer);";

			var parameters = new DynamicParameters();
			parameters.Add("Date", checkin.Date, DbType.DateTime);
			parameters.Add("IdCustomer", checkin.IdCustomer, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				return await db.QuerySingleAsync<int>(query, parameters);
			}
		}

		public Task<IEnumerable<Checkin>> GetByCustomer(int id)
		{
			throw new NotImplementedException();
		}
	}
}
