using LogixHealth.Eligibility.DataAccess.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.ConnectionProviders
{
    public class AppSettingsConnectionProvider : IConnectionProvider
    {

        private IConfiguration _configuration;
        

        public AppSettingsConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public TConnection Provide<TConnection>()
            where TConnection : class, IConnection
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var connectionString = _configuration.GetConnectionString("AppDbConnection");

            return new SqlConnectionAdapter(connectionString) as TConnection;
        }
    }
}
