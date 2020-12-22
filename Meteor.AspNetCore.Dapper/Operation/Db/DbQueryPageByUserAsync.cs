using System.Security.Claims;
using System.Text.Json.Serialization;
using Meteor.AspNetCore.Operation;
using Meteor.AspNetCore.Utils;
using Meteor.Database.Dapper.Operations.Db;

namespace Meteor.AspNetCore.Dapper.Operation.Db
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
    }
}