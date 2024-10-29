using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Shiny.Chainsaw.Repository.DbContext
{
	public class DapperContext : IDapperContext
	{
		IConfiguration _conf;
		public DapperContext(IConfiguration conf) 
		{
			_conf = conf;
		}

		public IDbConnection ConnectionCreate()
		{
			return new SqlConnection(_conf.GetConnectionString("Dapper"));
		}
	}
}
