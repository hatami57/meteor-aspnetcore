using Meteor.Database.Dapper.Operations.Db.Default;

namespace Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Queries
{
    public class GetUser : DbDefaultSelectAsync<int, User>
    {
        public GetUser() : base("user")
        {
        }
    }
}