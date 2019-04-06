using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces
{
    public interface IStatementFactoryProvider
    {
        IStatementFactory Provide();
    }
}
