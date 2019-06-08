using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface ISuffixConfiguration
	{
		IStrictNamingConfiguration WithInterfaceSuffixes(IEnumerable<string> suffixes);
		IStrictNamingConfiguration WithInterfaceSuffixes(params string[] suffixes);
        IStrictNamingConfiguration WithInterfaceSuffix(string suffix);
		IStrictNamingConfiguration WithoutInterfaceSuffixes();

	}
}