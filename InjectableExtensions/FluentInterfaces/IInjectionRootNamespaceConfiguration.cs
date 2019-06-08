using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IInjectionRootNamespaceConfiguration
	{
		IInjectionImplementationLimitsConfiguration WithRootNamespace(string name);
		IInjectionImplementationLimitsConfiguration WithRootNamespaces(params string[] names);
		IInjectionImplementationLimitsConfiguration WithRootNamespaces(IEnumerable<string> names);
	}
}