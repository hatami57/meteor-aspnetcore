using System.Threading.Tasks;
using Meteor.Message.Db;

namespace Meteor.AspCore.Message.Db.Default
{
    public class DbDefaultSelectByUserAsync<T> : DbMessageByUserAsync<T>
    {
        protected string TableName { get; set; }

        public DbDefaultSelectByUserAsync(string tableName)
        {
            TableName = tableName;
        }

        protected override Task<T> ExecuteMessageAsync() =>
            NewSql().SelectThisIdAsync<T>(TableName);
    }
}