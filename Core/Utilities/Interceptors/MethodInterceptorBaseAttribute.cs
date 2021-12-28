using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptorBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }


        public virtual void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

    }
}
