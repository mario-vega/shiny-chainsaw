using Dapper;
using Shiny.Chainsaw.Model;
using Shiny.Chainsaw.Repository.DbContext;
using System.Data;

namespace Shiny.Chainsaw.Repository
{
	public class PaymentHistoryRepository : IPaymentHistoryRepository
	{
		private readonly IDapperContext _dbcontext;

		public PaymentHistoryRepository(IDapperContext context)
		{
			_dbcontext = context;
		}

		public async void Add(PaymentHistory paymentHistory)
		{
			var query = @"INSERT INTO [dbo].[payment_history] ([Date], [Amount], [IdCustomer], [IdUser]) VALUES (@Name, @Telephone, @TelephoneEmergency);";

			var parameters = new DynamicParameters();
			parameters.Add("Date", paymentHistory.Date, DbType.DateTime);
			parameters.Add("Amount", paymentHistory.Amount, DbType.Decimal);
			parameters.Add("IdCustomer", paymentHistory.IdCustomer, DbType.Int32);
			parameters.Add("IdUser", paymentHistory.IdUser, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				await db.ExecuteAsync(query, parameters);
			}
		}

		public async Task<IEnumerable<PaymentHistory>> GetMonthly(DateTime refDate)
		{
			var initialDate = new DateTime(refDate.Year, refDate.Month, 1);
			var endDate = initialDate.AddMonths(1);
			IEnumerable<PaymentHistory> response;

			var parameters = new DynamicParameters();
			parameters.Add("initialDate", initialDate, DbType.DateTime);
			parameters.Add("endDate", endDate, DbType.DateTime);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				response = await db.QueryAsync<PaymentHistory>(sql: @"SELECT [Id], [Date], [Amount], [IdCustomer], [IdUser] FROM [shiny-chainsaw].[dbo].[payment_history] WHERE [Date] >= @initialDate AND [Date] < @endDate;", commandType: CommandType.Text);
			}
			return response;
		}

		public async void Remove(PaymentHistory paymentHistory)
		{
			var query = @"DELETE FROM [dbo].[payment_history] WHERE [Id] = @Id;";

			var parameters = new DynamicParameters();
			parameters.Add("Id", paymentHistory.Id, DbType.Int32);

			using (IDbConnection db = _dbcontext.ConnectionCreate())
			{
				await db.ExecuteAsync(query, parameters);
			}
		}
	}
}
