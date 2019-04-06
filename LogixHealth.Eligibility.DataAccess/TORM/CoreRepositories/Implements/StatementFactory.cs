using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Builder.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Mapper;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.MethodStament.Implements;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.MethodStament.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.PropertyMatcher.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Implements
{
    public class StatementFactory : IStatementFactory
    {
        private readonly IEntityMapper _entityMapper;
        private IConnectionProvider _connectionProvider;
        private readonly IWritablePropertyMatcher _writablePropertyMatcher;

        public StatementFactory(
            IConnectionProvider connectionProvider,
            IEntityMapper entityMapper,
             IWritablePropertyMatcher writablePropertyMatcher
            )
        {
            _connectionProvider = connectionProvider;
            _entityMapper = entityMapper;
            _writablePropertyMatcher = writablePropertyMatcher;
        }

        public ISelectStatement<TEntity> CreateSelect<TEntity>()
            where TEntity : class, new()
        {
            return new SelectStatement<TEntity>(this.CreateStatementExecutor(), _entityMapper);
        }

        public IInsertStatement<TEntity> CreateInsert<TEntity>()
            where TEntity : class, new()
        {
            return new InsertStatement<TEntity>(this.CreateStatementExecutor(),
                _entityMapper,
                _writablePropertyMatcher);
        }

        public IUpdateStatement<TEntity> CreateUpdate<TEntity>()
            where TEntity : class, new()
        {
            return new UpdateStatement<TEntity>(this.CreateStatementExecutor(),
                _entityMapper,
                _writablePropertyMatcher,
                new WhereClauseBuilder());
        }

        public IDeleteStatement<TEntity> CreateDelete<TEntity>()
           where TEntity : class, new()
        {
            return new DeleteStatement<TEntity>(this.CreateStatementExecutor(),
                _entityMapper,
                new WhereClauseBuilder());
        }

        public IStatementFactory UseConnectionProvider(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            return this;
        }

        private IStatementExecutor CreateStatementExecutor()
        {
            return new StatementExecutor(_connectionProvider);
        }
    }
}