using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Mapper;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.PropertyMatcher.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Implements
{
    public class StatementFactoryProvider : IStatementFactoryProvider
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEntityMapper _entityMapper;
        private readonly IWritablePropertyMatcher _writablePropertyMatcher;

        public StatementFactoryProvider(
            IEntityMapper entityMapper,
            IConnectionProvider connectionProvider,
            IWritablePropertyMatcher writablePropertyMatcher)
        {
            _entityMapper = entityMapper;
            _connectionProvider = connectionProvider;
            _writablePropertyMatcher = writablePropertyMatcher;
        }

        public IStatementFactory Provide()
        {
            return new StatementFactory(
                _connectionProvider,
                _entityMapper,
                _writablePropertyMatcher);
        }
    }
}