using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching.In_Memory
{
    public class CacheAspect : MethodInterceptor
    {
        private int _duration;
        private ICacheManager _cache;
        public CacheAspect(int duration)
        {

            _duration = duration;
            _cache = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = String.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({String.Join(",", arguments.Select(s => s?.ToString() ?? "<NULL>"))})";
            if (_cache.IsAdded(key))
            {
                invocation.ReturnValue = _cache.Get(key);
                return;
            }

            invocation.Proceed();
            var result = invocation.ReturnValue as Task;
            result?.Wait();
            _cache.Add(key, invocation.ReturnValue, _duration);

        }
    }
}
