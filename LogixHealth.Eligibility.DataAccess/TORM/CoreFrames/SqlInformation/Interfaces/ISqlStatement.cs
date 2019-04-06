using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.Common;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Builder.Interfaces;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.SqlInformation.Interfaces
{
    public interface ISqlStatement<TResult> : IClauseBuilder
    {
        string TableName { get; }
        string TableSchema { get; }

        TResult Go();

        Task<TResult> GoAsync();

        TResult GoStoreProcedure(string name,
            params ParameterDefinition[] parametersDefinitions);

        Task<TResult> GoStoreProcedureAsync(string name,
            params ParameterDefinition[] parametersDefinitions);

        ISqlStatement<TResult> UseConnectionProvider(IConnectionProvider connectionProvider);
    }
}