using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching.In_Memory
{
    public class CacheRemoveAspect : MethodInterceptor
    {
        private string _pattern;
        private ICacheManager _cache;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cache = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void OnSuccess(IInvocation invocation)
        {
            _cache.RemoveByPattern(_pattern);
        }
    }
}
