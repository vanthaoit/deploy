using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.MethodStament.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using System.Collections.Generic;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Implements
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        private readonly IStatementFactory statementFactory;

        public RepositoryBase(IStatementFactory statementFactory)
        {
            this.statementFactory = statementFactory;
        }

        public ISelectStatement<TEntity> Query()
        {
            return this.statementFactory.CreateSelect<TEntity>();
        }

        public IEnumerable<TEntity> ResultsFrom(ISelectStatement<TEntity> query)
        {
            return query.Go();
        }

        public TEntity Insert(TEntity entity)
        {
            return this.statementFactory.CreateInsert<TEntity>()
                       .For(entity)
                       .Go();
        }

        public IInsertStatement<TEntity> Insert()
        {
            return this.statementFactory.CreateInsert<TEntity>();
        }

        public IUpdateStatement<TEntity> Update()
        {
            return this.statementFactory.CreateUpdate<TEntity>();
        }

        public int Update(TEntity entity)
        {
            return this.statementFactory.CreateUpdate<TEntity>()
                       .For(entity)
                       .Go();
        }

        public IDeleteStatement<TEntity> Delete()
        {
            return this.statementFactory.CreateDelete<TEntity>();
        }

        public int Delete(TEntity entity)
        {
            return this.statementFactory.CreateDelete<TEntity>()
                       .For(entity)
                       .Go();
        }

        public IRepositoryBase<TEntity> UseConnectionProvider(IConnectionProvider connectionProvider)
        {
            this.statementFactory.UseConnectionProvider(connectionProvider);
            return this;
        }
    }
}