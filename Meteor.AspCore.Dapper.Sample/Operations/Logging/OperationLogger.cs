using System;
using System.Text.Json;
using System.Threading.Tasks;
using Meteor.AspCore.Dapper.Sample.Operations.Logging.Types;
using Meteor.Database.Dapper;
using Meteor.Operation;
using Serilog;

namespace Meteor.AspCore.Dapper.Sample.Operations.Logging
{
    public partial class OperationLogger : IOperationLoggerAsync
    {
        private readonly LazyDbConnection _lazyDbConnection;

        public OperationLogger(LazyDbConnection lazyDbConnection)
        {
            _lazyDbConnection = lazyDbConnection;
        }

        public Task LogAsync(IOperationAsync operation)
        {
            //this.GeneratedMethod();
            Log.Information("Log: Operation '{Name}({State})', Input: {@Input}, Output: {@Output}",
                operation.GetType().Name, operation.State, operation.Input, operation.Output);

            if (operation is not ILog op) return Task.CompletedTask;
            
            var type = op switch
            {
                ILogSelect => "Select",
                ILogInsert => "Insert",
                ILogUpdate => "Update",
                ILogDelete => "Delete",
                _ => "General"
            };
            Console.WriteLine($"[LOG] (UserId: {op.LogDetails.UserId}) [{type}] => {op.LogDetails.Result} (Input: {JsonSerializer.Serialize(op.LogDetails.Input)}, Output: {JsonSerializer.Serialize(op.LogDetails.Output)})");
            return Task.CompletedTask;
        }
    }
}