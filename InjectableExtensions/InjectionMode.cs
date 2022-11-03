namespace Injectable.NetCore.Extensions
{
    /// <summary>
    /// Injection Modes
    /// </summary>
    public enum InjectionMode
    {
        /// <summary>
        /// Triggers the use of scoped injection
        /// </summary>
        Scoped,
        /// <summary>
        /// Triggers the use of singleton injection
        /// </summary>
        Singleton,
        /// <summary>
        /// Triggers the use of transient injection
        /// </summary>
        Transient
    }
}
