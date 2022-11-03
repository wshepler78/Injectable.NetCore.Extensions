using Microsoft.Extensions.DependencyInjection;

namespace Injectable.NetCore.Extensions
{
    /// <summary>
    /// Injection Manager Interface
    /// </summary>
    public interface IInjectionManager
    {
        /// <summary>
        /// Injects the services into the IServiceCollection.
        /// </summary>
        /// <param name="services">The services.</param>
        void InjectServices (IServiceCollection services);
    }
}
