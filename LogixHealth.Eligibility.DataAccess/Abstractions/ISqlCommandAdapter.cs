using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.DataAccess.Abstractions
{
    public interface ISqlCommandAdapter : IDisposable
    {
        int CommandTimeout { get; set; }
        CommandType CommandType { get; set; }
        string CommandText { get; set; }
        ISqlParameterCollection Parameters { get; }
        int ExecuteNonQuery();
        Task<int> ExecuteNonQueryAsync();
        IDataReader ExecuteReader(CommandBehavior closeConnection);
        Task<IDataReader> ExecuteReaderAsync(CommandBehavior closeConnection);
    }
}
