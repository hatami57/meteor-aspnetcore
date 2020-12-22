using Meteor.Database.Dapper.Operations.Db.Default;

namespace Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Queries
{
    public class GetUserPage : DbDefaultQueryPageAsync<User>
    {
        public GetUserPage() : base("user")
        {
        }
    }
}