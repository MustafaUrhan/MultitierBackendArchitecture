using Core.DependencyResolver.Abstract;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extentions
{
    public static class ServiceCollectionExtentions
    {

        public static IServiceCollection AddDepencenyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }
            return ServiceTool.Create(services);
        }
    }
}
