using Meteor.Database.Dapper;
using Meteor.Database.Sql.SqlDialect;
using Microsoft.Extensions.DependencyInjection;

namespace Meteor.AspNetCore.Dapper
{
    public static class Extensions
    {
        public static IServiceCollection AddDbOperation<TDbConnectionFactory, TSqlDialect>(this IServiceCollection services)
            where TDbConnectionFactory : class, IDbConnectionFactory
            where TSqlDialect : ISqlDialect, new()
        {
            services.AddSingleton<IDbConnectionFactory, TDbConnectionFactory>();
            services.AddSingleton<ISqlFactory, SqlFactory<TSqlDialect>>();
            services.AddScoped<LazyDbConnection>();

            return services;
        }
    }
}