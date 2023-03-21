using Microsoft.Data.SqlClient;
using System.Data;

namespace WebUsingDapper.Connections
{
    public class DapperDbContext
    {
        private IConfiguration _configuration;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection connection =>new SqlConnection(_configuration.GetConnectionString("DapperDb"));
    }
}
