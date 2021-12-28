using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolver.Abstract
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
