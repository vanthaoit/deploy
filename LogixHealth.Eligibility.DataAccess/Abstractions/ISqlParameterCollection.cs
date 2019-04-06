using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Abstractions
{
    public interface ISqlParameterCollection
    {
        void AddWithValue(string name, object value);
    }
}
