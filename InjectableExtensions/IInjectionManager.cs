using Microsoft.Extensions.DependencyInjection;

namespace Injectable.WS.Extensions
{
    public interface IInjectionManager
    {
        void InjectServices (IServiceCollection services);
    }
}
