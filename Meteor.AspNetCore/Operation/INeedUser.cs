using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Meteor.AspNetCore.Operation
{
    public interface INeedUser
    {
        [JsonIgnore]
        ClaimsPrincipal ByUser { get; set; }
    }
}