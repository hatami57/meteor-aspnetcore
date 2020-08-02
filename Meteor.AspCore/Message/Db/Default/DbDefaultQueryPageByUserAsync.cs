using System.Threading.Tasks;
using Meteor.Message.Db;

namespace Meteor.AspCore.Message.Db.Default
{
    public class DbDefaultQueryPageByUserAsync<T> : DbQueryPageByUserAsync<T>
    {
        protected string TableName { get; set; }
        
        public DbDefaultQueryPageByUserAsync(string tableName)
        {
            TableName = tableName;
        }
        protected override Task<QueryPage<T>> ExecuteMessageAsync() => 
            NewSql().SelectQueryPageAsync(TableName, this);
    }
}