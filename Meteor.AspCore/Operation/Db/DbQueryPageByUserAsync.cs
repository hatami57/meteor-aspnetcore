using System.Security.Claims;
using System.Text.Json.Serialization;
using Meteor.AspCore.Utils;
using Meteor.Database;
using Meteor.Database.SqlDialect;
using Meteor.Operation.Db;

namespace Meteor.AspCore.Operation.Db
{
    public abstract class DbQueryPageByUserAsync<TInput, TOutput> : DbQueryPageAsync<TInput, TOutput>, INeedUser where TInput : IQueryPageInput
    {
        private ClaimsPrincipal _byUser;

        [JsonIgnore]
        public ClaimsPrincipal ByUser
        {
            get => _byUser;
            set
            {
                _byUser = value;
                ByUserId = _byUser?.GetUserId() ?? 0;
            }
        }

        public long ByUserId { get; private set; }

        protected DbQueryPageByUserAsync(LazyDbConnection lazyDbConnection, ISqlFactory sqlFactory) : base(lazyDbConnection, sqlFactory)
        {
        }
    }
}