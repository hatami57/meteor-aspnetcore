using System.IO;
using System.Threading.Tasks;
using Meteor.AspCore.Dapper.Sample.Operations.Db;
using Meteor.AspCore.Dapper.Sample.Operations.Logging;
using Meteor.Database.Dapper.Sqlite;
using Meteor.Database.Dapper.Sqlite.SqlDialect.Sqlite;
using Meteor.Operation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Meteor.AspCore.Dapper.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            Directory.CreateDirectory("data");
            EnvVars.SetDefaultValue(EnvVarKeys.DbUri, "data source=data/main.db");
            
            services.AddOperationFactory()
                .AddDbOperation<SqliteDbConnectionFactory, SqliteDialect>()
                .AddScopedOperationLogger<OperationLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DefaultOperationSettings.LoggerAsync = operation =>
            {
                Log.Information("** INSIDE Default Logger");
                return Task.CompletedTask;
            };

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            using var scope = app.ApplicationServices.CreateScope();
            scope.ServiceProvider.GetRequiredService<OperationFactory>()
                .ExecuteAsync<CreateDatabase>()
                .Wait();
        }
    }
}
