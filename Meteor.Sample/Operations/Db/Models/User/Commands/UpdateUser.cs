using System.Threading.Tasks;
using Meteor.Operation.Db;
using Meteor.Sample.Operations.Logging;
using Meteor.Sample.Operations.Logging.Types;

namespace Meteor.Sample.Operations.Db.Models.User.Commands
{
    [DbOperation("int Id; string FirstName; string LastName; string Username;", typeof(bool))]
    public partial class UpdateUser : ILogUpdate
    {
        public LogDetails LogDetails { get; private set; }
        
        protected override async Task ExecutionAsync()
        {
            Output = await NewSql(sql => sql.UpdateThisId("user", "first_name=@FirstName, last_name=@LastName, username=@Username"))
                .ExecuteAsync() > 0;
            
            LogDetails = new LogDetails
            {
                UserId = 5,
                Result = Output,
                Input = Input,
                Output = Output
            };
        }
    }
}
