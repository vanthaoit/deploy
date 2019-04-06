using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.DataAccess.Abstractions
{
    public interface ISqlConnectionAdapter :IConnection
    {
        void Open();
        ISqlCommandAdapter CreateCommand();

        Task OpenAsync();
    }
}
