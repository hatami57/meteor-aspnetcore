using System.Security.Claims;
using System.Text.Json.Serialization;
using Meteor.AspCore.Utils;
using Meteor.Message;

namespace Meteor.AspCore.Message
{
    public abstract class MessageByUserAsync<T> : MessageAsync<T>, INeedUser
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
