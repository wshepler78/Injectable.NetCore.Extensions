namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Injection Mode Configuration
	/// </summary>
	public interface IInjectionModeConfiguration
	{
		/// <summary>
		/// Sets the injection mode to use.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <returns></returns>
		IInjectionRootNamespaceConfiguration WithInjectionMode(InjectionMode mode);
	}
}