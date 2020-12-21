using Meteor.Database.Dapper.Operations.Db.Default;

namespace Meteor.AspCore.Dapper.Sample.Operations.Db.Models.User.Queries
{
    public class GetUser : DbDefaultSelectAsync<int, User>
    {
        public GetUser() : base("user")
        {
        }
    }
}