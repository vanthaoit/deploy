using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.MethodStament.Interfaces;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces
{
    public interface IStatementFactory
    {
        ISelectStatement<TEntity> CreateSelect<TEntity>()
           where TEntity : class, new();

        IInsertStatement<TEntity> CreateInsert<TEntity>()
           where TEntity : class, new();

        IUpdateStatement<TEntity> CreateUpdate<TEntity>()
            where TEntity : class, new();

        IDeleteStatement<TEntity> CreateDelete<TEntity>()
            where TEntity : class, new();

        IStatementFactory UseConnectionProvider(IConnectionProvider connectionProvider);
    }
}