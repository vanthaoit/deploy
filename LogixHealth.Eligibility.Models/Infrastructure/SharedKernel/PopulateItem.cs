using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LogixHealth.Eligibility.Models.Infrastructure.SharedKernel
{
    public abstract class PopulateItem<T>
    {
        public abstract T PopulateRecord(SqlDataReader reader);
    }
}
