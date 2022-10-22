using ECommerceAPI.API.ColumnModel;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.API.LoggerConfigurationHandler
{
    public static class LoggerConfigurationHandler
    {
        public static Logger GetConfiguratedLogger(IConfiguration configuration)
        {
            SqlColumn sqlColumn = new SqlColumn();
            sqlColumn.ColumnName = "UserName";
            sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
            sqlColumn.PropertyName = "UserName";
            sqlColumn.DataLength = 50;
            sqlColumn.AllowNull = true;
            ColumnOptions columnOpt = new ColumnOptions();
            columnOpt.Store.Remove(StandardColumn.Properties);
            columnOpt.Store.Add(StandardColumn.LogEvent);
            columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

            Logger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString("DBConnectionString"),
                 sinkOptions: new MSSqlServerSinkOptions
                 {
                     AutoCreateSqlTable = true,
                     TableName = "Logs",
                 },
                 appConfiguration: null,
                 columnOptions: columnOpt

                )
                .Enrich.FromLogContext()
                .Enrich.With<CustomUserNameColumn>()
                .MinimumLevel.Information()
                .CreateLogger();
            return logger;
        }

    }
}

