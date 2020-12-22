using System;
using System.Linq;
using Meteor.Operation;
using Meteor.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

namespace Meteor.AspNetCore.Filters
{
    public sealed class OperationResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Filters.OfType<IgnoreOperationResultAttribute>().Any())
                return;
            
            if (context.Exception != null)
            {
                context.Result = GetResultFromException(context.Exception);
                Log.Error(context.Exception, "action exception");
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = GetResultFromActionResult(context.Result);
            }
        }

        private static IActionResult GetResultFromException(Exception exception)
        {
            var error = exception is Error err ? err : Errors.InternalError(exception.Message, exception);
            return GetErrorResult(new OperationResult(false, error));
        }

        private static IActionResult GetResultFromActionResult(IActionResult actionResult)
        {
            return actionResult switch
            {
                StatusCodeResult result => GetResultFromCondition(IsSuccessful(result.StatusCode)),
                ObjectResult result => GetResultFromObjectResult(result),
                EmptyResult _ => GetResultFromCondition(true),
                _ => actionResult
            };
        }

        private static IActionResult GetResultFromObjectResult(ObjectResult result)
        {
            return result.Value is OperationResult oRes
                ? GetResultFromCondition(oRes.Success, oRes, oRes)
                : GetSuccessResult(new OperationResult<object>(IsSuccessful(result), result.Value));
        }

        private static IActionResult GetResultFromCondition(bool success, OperationResult successValue,
            OperationResult errorValue) =>
            success ? GetSuccessResult(successValue) : GetErrorResult(errorValue);

        private static IActionResult GetResultFromCondition(bool success) =>
            GetResultFromCondition(success, new OperationResult(true), new OperationResult(false));
        
        private static IActionResult GetSuccessResult(OperationResult result) =>
            new OkObjectResult(result);
        
        private static IActionResult GetErrorResult(OperationResult result) =>
            new ConflictObjectResult(result);

        private static bool IsSuccessful(IStatusCodeActionResult result) =>
            result.StatusCode == null || IsSuccessful(result.StatusCode.Value);

        private static bool IsSuccessful(int statusCode) => statusCode >= 200 && statusCode <= 299;
    }
}