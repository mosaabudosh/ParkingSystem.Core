using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ParkingSystem.Application.Security
{
    public class CustomValueProviderFactories : IValueProviderFactory
    {
        public CustomValueProviderFactories()
        {
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new CustomQueryStringValueProvider(context.ActionContext.HttpContext.Request));
            return Task.CompletedTask;
        }
    }
}
