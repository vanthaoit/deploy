using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepositoryBase<TEntity> Create<TEntity>() where TEntity : class, new();
    }
}
