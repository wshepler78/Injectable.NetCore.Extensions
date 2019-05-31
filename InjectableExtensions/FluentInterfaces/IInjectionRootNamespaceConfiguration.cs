using System.Collections.Generic;

namespace Injectable.WS.Extensions
{
	public interface IInjectionRootNamespaceConfiguration
	{
		IInjectionImplementationLimitsConfiguration WithRootNamespace(string name);
		IInjectionImplementationLimitsConfiguration WithRootNamespaces(IEnumerable<string> names);
	}
}