using System.Threading.Tasks;
using Meteor.AspNetCore.Dapper.Sample.Operations.Logging;
using Meteor.AspNetCore.Dapper.Sample.Operations.Logging.Types;
using Meteor.Database.Dapper.Operations.Db;

namespace Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Commands
{
    [DbOperation("string FirstName; string LastName; string Username;", typeof(int))]
    public partial class AddUser : ILogInsert
    {
        public LogDetails LogDetails { get; private set; }

        protected override async Task ExecutionAsync()
        {
            Output = await NewSql(sql => sql.InsertReturnId("user",
                    "first_name, last_name, username",
                    "@FirstName, @LastName, @Username"))
                .ExecuteScalarAsync<int>();

            LogDetails = new LogDetails
            {
                UserId = 5,
                Result = Output > 0,
                Input = Input,
                Output = Output
            };
        }
    }
}
