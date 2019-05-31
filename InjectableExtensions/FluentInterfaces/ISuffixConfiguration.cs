using System.Collections.Generic;

namespace Injectable.WS.Extensions
{
	public interface ISuffixConfiguration
	{
		IStrictNamingConfiguration WithInterfaceSuffixes(IEnumerable<string> suffixes);
		IStrictNamingConfiguration WithInterfaceSuffix(string suffix);
		IStrictNamingConfiguration WithoutInterfaceSuffixes();

	}
}