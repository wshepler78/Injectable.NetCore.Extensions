namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Configures Interface Prefix to look for
	/// </summary>
	public interface IPrefixConfiguration
	{
		/// <summary>
		/// Sets the interface prefix to the supplied string
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <returns></returns>
		ISuffixConfiguration WithInterfacePrefix(string prefix);
		/// <summary>
		/// Looks for interfaces that start with "I"
		/// </summary>
		/// <returns></returns>
		ISuffixConfiguration WithDefaultInterfacePrefix();
	}
}