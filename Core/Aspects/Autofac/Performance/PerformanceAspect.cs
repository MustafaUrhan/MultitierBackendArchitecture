using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterceptor
    {
        private readonly int _elapseSeconds;
        private Stopwatch _stopWatch;
        public PerformanceAspect(int elapseSecond)
        {
            _elapseSeconds = elapseSecond;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            _stopWatch.Start();
        }

        public override void OnAfter(IInvocation invocation)
        {
            if (_stopWatch.Elapsed.TotalSeconds > _elapseSeconds)
            {
                Debug.WriteLine($"Performance issiues occured on {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ---> {_stopWatch.Elapsed.TotalSeconds}");
            }
            _stopWatch.Reset();
        }
    }
}
