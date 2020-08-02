using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Meteor.AspCore.Message
{
    public interface INeedUser
    {
        [JsonIgnore]
        ClaimsPrincipal ByUser { get; set; }
    }
}