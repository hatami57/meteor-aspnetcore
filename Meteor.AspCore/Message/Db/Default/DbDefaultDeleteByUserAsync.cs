using System.Threading.Tasks;
using Meteor.Message.Db;

namespace Meteor.AspCore.Message.Db.Default
{
    public class DbDefaultDeleteByUserAsync : DbMessageByUserAsync<bool>
    {
        protected string TableName { get; set; }

        public DbDefaultDeleteByUserAsync(string tableName)
        {
            TableName = tableName;
        }

        protected override async Task<bool> ExecuteMessageAsync() =>
            await NewSql().DeleteThisIdAsync(TableName)
                .ConfigureAwait(false) > 0;
    }
}