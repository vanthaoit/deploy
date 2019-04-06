using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.DataAccess.Abstractions
{
    public class SqlConnectionAdapter : ISqlConnectionAdapter
    {
        private readonly SqlConnection _connection;

        public SqlConnectionAdapter(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public SqlConnectionAdapter()
        {
            _connection = new SqlConnection();
        }

     

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void Open()
        {
            _connection.Open();
        }

        public Task OpenAsync()
        {
            return _connection.OpenAsync();
        }

        public ISqlCommandAdapter CreateCommand()
        {
            return new SqlCommandAdapter(_connection.CreateCommand());
        }
    }
}
