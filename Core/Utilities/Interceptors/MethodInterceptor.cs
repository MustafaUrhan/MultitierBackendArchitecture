using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterceptor : MethodInterceptorBaseAttribute
    {

        public virtual void OnBefore(IInvocation invocation) { }
        public virtual void OnAfter(IInvocation invocation) { }
        public virtual void OnSuccess(IInvocation invocation) { }
        public virtual void OnException(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var success = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                OnException(invocation);
                success = false;
                throw;
            }
            finally
            {
                if (success)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
       
    }
}
