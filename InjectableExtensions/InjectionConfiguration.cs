namespace Injectable.NetCore.Extensions
{
    /// <summary>
    /// Interface specifying the an Injection Configuration
    /// </summary>
    public class InjectionConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="IInjectionManager"/>.
        /// </summary>
        public IInjectionManager Manager { get; set; }
    }
}
