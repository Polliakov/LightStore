using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace LightStore.WebApi.Binding
{
    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            var valueAsString = value.FirstValue;
            try
            {
                var result = JsonSerializer.Deserialize(valueAsString, bindingContext.ModelType, 
                    new JsonSerializerOptions 
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result != null)
                    bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }          
            return Task.CompletedTask;
        }
    }
}
