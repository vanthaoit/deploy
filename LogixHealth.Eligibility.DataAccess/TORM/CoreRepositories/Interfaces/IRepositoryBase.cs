using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.MethodStament.Interfaces;
using System.Collections.Generic;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        ISelectStatement<TEntity> Query();

        IEnumerable<TEntity> ResultsFrom(ISelectStatement<TEntity> query);

        IInsertStatement<TEntity> Insert();

        TEntity Insert(TEntity entity);

        IUpdateStatement<TEntity> Update();

        int Update(TEntity entity);

        IDeleteStatement<TEntity> Delete();
        int Delete(TEntity entity);

        IRepositoryBase<TEntity> UseConnectionProvider(IConnectionProvider connectionProvider);
    }
}