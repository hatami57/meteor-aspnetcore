using System.Threading.Tasks;
using Meteor.Operation.Db;
using Meteor.Sample.Operations.Logging;
using Meteor.Sample.Operations.Logging.Types;

namespace Meteor.Sample.Operations.Db.Models.User.Commands
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
