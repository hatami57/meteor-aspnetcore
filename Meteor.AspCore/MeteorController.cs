using Meteor.AspCore.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Meteor.AspCore
{
    [ApiController]
    [OperationResult]
    public class MeteorController : ControllerBase
    {
        protected readonly IDiagnosticContext LogContext;
        
        public MeteorController(IDiagnosticContext diagnosticContext)
        {
            LogContext = diagnosticContext;
        }

        protected void LogInputObject(object input) => 
            LogContext.Set("input", input, true);
    }
}