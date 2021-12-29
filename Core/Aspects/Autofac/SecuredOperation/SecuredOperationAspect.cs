using Castle.DynamicProxy;
using Core.Extentions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.SecuredOperation
{
    public class SecuredOperationAspect : MethodInterceptor
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperationAspect(string roles)
        {
            if (string.IsNullOrEmpty(roles))
            {
                throw new ArgumentNullException(AspectMessages.SecuredOperationAspectArgumentsEmpty);
            }
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            var claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (claimRoles.Contains(role))
                {
                    return;
                }

            }
            throw new UnauthorizedAccessException(AspectMessages.SecuredOperationUnAuthorized);
        }
    }
}
