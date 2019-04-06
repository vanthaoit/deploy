using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Implements
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IStatementFactoryProvider statementFactoryProvider;

        public RepositoryFactory(IStatementFactoryProvider statementFactoryProvider)
        {
            this.statementFactoryProvider = statementFactoryProvider;
        }

        public IRepositoryBase<TEntity> Create<TEntity>() where TEntity : class, new()
        {
            return new RepositoryBase<TEntity>(this.statementFactoryProvider.Provide());
        }
    }
}
