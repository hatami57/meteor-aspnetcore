using System.Security.Claims;
using System.Text.Json.Serialization;
using Meteor.AspNetCore.Utils;
using Meteor.Operation;

namespace Meteor.AspNetCore.Operation
{
    public abstract class OperationByUserAsync<TInput, TOutput> : OperationAsync<TInput, TOutput>, INeedUser
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
