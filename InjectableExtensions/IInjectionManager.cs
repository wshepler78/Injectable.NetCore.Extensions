using Microsoft.Extensions.DependencyInjection;

namespace Injectable.NetCore.Extensions
{
    public interface IInjectionManager
    {
        void InjectServices (IServiceCollection services);
    }
}
