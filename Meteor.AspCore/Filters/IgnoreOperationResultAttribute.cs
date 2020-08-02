using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meteor.AspCore.Filters
{
    public sealed class IgnoreOperationResultAttribute : Attribute, IFilterMetadata
    {
    }
}