using Dapper;
using Shiny.Chainsaw.Model;
using Shiny.Chainsaw.Repository.DbContext;
using System.Data;

namespace Shiny.Chainsaw.Repository
{
	public class CheckinHistoryRepository : ICheckinHistoryRepository
	{
		private readonly IDapperContext _dbcontext;

		public CheckinHistoryRepository(IDapperContext context)
		{
			_dbcontext = context;
		}

		public async void Add(CheckinHistory checkin)
		{
			var query = @"INSERT INTO [dbo].[payment_history] ([Date], [Amount], [IdCustomer], [IdUser]) VALUES (@Name, @Telephone, @TelephoneEmergency);";

			var parameters = new DynamicParameters();
			parameters.Add("Date", checkin.Date, DbType.DateTime);
			parameters.Add("IdCustomer", checkin.IdCustomer, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				await db.ExecuteAsync(query, parameters);
			}
		}

		public Task<IEnumerable<CheckinHistory>> GetByCustomer(int id)
		{
			throw new NotImplementedException();
		}
	}
}
