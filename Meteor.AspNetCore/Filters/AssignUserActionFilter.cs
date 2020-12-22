using Meteor.AspNetCore.Operation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meteor.AspNetCore.Filters
{
    public class AssignUserActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value is INeedUser user)
                    user.ByUser = context.HttpContext.User;
            }
        }
    }
}