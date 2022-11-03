using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Configures Interface Suffixes
	/// </summary>
	public interface ISuffixConfiguration
	{
		/// <summary>
		/// Locates only interfaces with the supplied suffixes.
		/// </summary>
		/// <param name="suffixes">The suffixes.</param>
		/// <returns></returns>
		IStrictNamingConfiguration WithInterfaceSuffixes(IEnumerable<string> suffixes);
        /// <summary>
        /// Locates only interfaces with the supplied suffixes.
        /// </summary>
        /// <param name="suffixes">The suffixes.</param>
        /// <returns></returns>
        IStrictNamingConfiguration WithInterfaceSuffixes(params string[] suffixes);
        /// <summary>
        /// Locates only interfaces with the supplied suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        IStrictNamingConfiguration WithInterfaceSuffix(string suffix);
		/// <summary>
		/// Loads all interfaces regardless of suffix
		/// </summary>
		/// <returns></returns>
		IStrictNamingConfiguration WithoutInterfaceSuffixes();

	}
}