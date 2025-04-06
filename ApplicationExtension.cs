using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;

namespace SerilogStructuredLogging
{
    public static class ApplicationExtension
    {
        public static void ConfigureSerilog(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) =>
            {
                var connectionString = ctx.Configuration.GetConnectionString("DefaultConnection");

                lc.WriteTo.Console();
                lc.WriteTo.Seq("http://localhost:5341");
                lc.WriteTo.File(new JsonFormatter(), "Log.txt");
                lc.WriteTo.MSSqlServer(
                    connectionString: connectionString, sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Log",
                    SchemaName = "dbo",
                    AutoCreateSqlTable = true
                });

            });
        }
    }
}
