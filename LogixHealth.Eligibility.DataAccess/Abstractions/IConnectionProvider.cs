using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Abstractions
{
    public interface IConnectionProvider
    {
        TConnection Provide<TConnection>() where TConnection : class, IConnection;
    }
}
