using Meteor.Database.Dapper.Operations.Db.Default;

namespace Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Commands
{
    public class DeleteUser : DbDefaultDeleteAsync<int>
    {
        public DeleteUser() : base("user")
        {
        }
    }
}
