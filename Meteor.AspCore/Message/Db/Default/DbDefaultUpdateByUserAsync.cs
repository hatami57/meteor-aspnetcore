using System.Threading.Tasks;
using Meteor.Message.Db;

namespace Meteor.AspCore.Message.Db.Default
{
    public class DbDefaultUpdateByUserAsync : DbMessageByUserAsync<bool>
    {
        protected string TableName { get; set; }
        protected string SetFields { get; set; }

        public DbDefaultUpdateByUserAsync(string tableName, string setFields)
        {
            TableName = tableName;
            SetFields = setFields;
        }

        protected override async Task<bool> ExecuteMessageAsync() =>
            await NewSql().UpdateThisIdAsync(TableName, SetFields)
                .ConfigureAwait(false) > 0;
    }
}