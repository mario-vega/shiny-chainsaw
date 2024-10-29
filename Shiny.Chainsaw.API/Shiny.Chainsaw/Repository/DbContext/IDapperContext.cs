using System.Data;

namespace Shiny.Chainsaw.Repository.DbContext
{
	public interface IDapperContext
	{
		public IDbConnection ConnectionCreate();
	}
}
