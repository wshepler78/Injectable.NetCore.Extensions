using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IInjectionRootNamespaceConfiguration
	{
		IInjectionImplementationLimitsConfiguration WithRootNamespace(string name);
		IInjectionImplementationLimitsConfiguration WithRootNamespaces(IEnumerable<string> names);
	}
}