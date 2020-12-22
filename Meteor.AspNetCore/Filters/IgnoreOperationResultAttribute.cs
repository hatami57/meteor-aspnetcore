using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meteor.AspNetCore.Filters
{
    public sealed class IgnoreOperationResultAttribute : Attribute, IFilterMetadata
    {
    }
}