using Meteor.Database.Dapper.Operations.Db.Default;

namespace Meteor.AspCore.Dapper.Sample.Operations.Db.Models.User.Queries
{
    public class GetUserPage : DbDefaultQueryPageAsync<User>
    {
        public GetUserPage() : base("user")
        {
        }
    }
}