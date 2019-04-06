using LogixHealth.Eligibility.DataAccess.Abstractions;
using LogixHealth.Eligibility.DataAccess.Common;
using LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Executor.Implements
{
    public class StatementExecutor : IStatementExecutor
    {
        private const int CommandTimeout = 300000;

        private IConnectionProvider connectionProvider;

        public StatementExecutor(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public int ExecuteNonQuery(string sql)
        {
            this.LogQuery(sql);
            using (var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = CommandTimeout;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    return command.ExecuteNonQuery();
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sql)
        {
            this.LogQuery(sql);
            using (var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = CommandTimeout;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public int ExecuteNonQueryStoredProcedure(string name,
            params ParameterDefinition[] parameterDefinitions)
        {
            this.LogExecuteProc(name);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = name;
                foreach (var parameterDefinition in parameterDefinitions)
                {
                    command.Parameters.AddWithValue(parameterDefinition.Name, parameterDefinition.Value);
                }

                return command.ExecuteNonQuery();
            }
        }

        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string name,
            params ParameterDefinition[] parameterDefinitions)
        {
            this.LogExecuteProc(name);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = name;
                foreach (var parameterDefinition in parameterDefinitions)
                {
                    command.Parameters.AddWithValue(parameterDefinition.Name, parameterDefinition.Value);
                }

                return await command.ExecuteNonQueryAsync();
            }
        }

        public IDataReader ExecuteReader(string sql)
        {
            this.LogQuery(sql);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public async Task<IDataReader> ExecuteReaderAsync(string sql)
        {
            this.LogQuery(sql);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public IDataReader ExecuteStoredProcedure(string name,
            params ParameterDefinition[] parametersDefinitions)
        {
            this.LogExecuteProc(name);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = name;
                foreach (var parametersDefinition in parametersDefinitions)
                {
                    command.Parameters.AddWithValue(parametersDefinition.Name, parametersDefinition.Value);
                }

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public async Task<IDataReader> ExecuteStoredProcedureAsync(string name,
            params ParameterDefinition[] parametersDefinitions)
        {
            this.LogExecuteProc(name);
            var connection = this.connectionProvider.Provide<ISqlConnectionAdapter>();
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = name;
                foreach (var parametersDefinition in parametersDefinitions)
                {
                    command.Parameters.AddWithValue(parametersDefinition.Name, parametersDefinition.Value);
                }

                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public IStatementExecutor UseConnectionProvider(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
            return this;
        }

        private void LogExecuteProc(string name)
        {
            Console.WriteLine($"Executing SP: {name}");
        }

        private void LogQuery(string sql)
        {
            Console.WriteLine($"Executing SQL:{Environment.NewLine}{sql}");
        }
    }
}