
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolver.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolver.Concrete
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
