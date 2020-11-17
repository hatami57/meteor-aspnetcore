using Meteor.Database;
using Meteor.Database.SqlDialect;
using Meteor.Database.SqlDialect.Sqlite;
using Meteor.Database.Sqlite;
using Meteor.Operation;
using Microsoft.Extensions.DependencyInjection;

namespace Meteor.AspCore
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