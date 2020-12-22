using System;
using System.Threading.Tasks;
using Meteor.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Meteor.AspNetCore.ModelBinders
{
    public class LocalDateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var modelState = bindingContext.ModelState;
            modelState.SetModelValue(modelName, valueProviderResult);

            var metadata = bindingContext.ModelMetadata;
            var value = valueProviderResult.FirstValue;
            var model = string.IsNullOrWhiteSpace(value) ? null : value.ToLocalDate();

            if (model == null)
            {
                if (!metadata.IsReferenceOrNullableType)
                    modelState.TryAddModelError(modelName,
                        metadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(
                            valueProviderResult.ToString()));
                else
                    bindingContext.Result = ModelBindingResult.Success(null);
            }
            else
            {
                bindingContext.Result = model.Success
                    ? ModelBindingResult.Success(model.Value)
                    : ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}