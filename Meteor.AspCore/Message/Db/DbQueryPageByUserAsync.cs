using System.Security.Claims;
using System.Text.Json.Serialization;
using Meteor.AspCore.Utils;
using Meteor.Database;
using Meteor.Message.Db;

namespace Meteor.AspCore.Message.Db
{
    public abstract class DbQueryPageByUserAsync<T> : DbQueryPageAsync<T>, INeedUser
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

        protected DbQueryPageByUserAsync(LazyDbConnection lazyDbConnection) : base(lazyDbConnection)
        {
        }

        protected DbQueryPageByUserAsync() : this(null)
        {
        }
    }
}