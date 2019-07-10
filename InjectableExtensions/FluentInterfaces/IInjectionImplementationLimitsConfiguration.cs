using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IInjectionImplementationLimitsConfiguration
	{
		IPrefixConfiguration LimitImplementationsToInterfaceNamespace();
		IPrefixConfiguration AllowImplementationsInAnyNamespace();
		IPrefixConfiguration AllowImplementationsInNamespaces(params string[] implementationNamespaces);
		IPrefixConfiguration AllowImplementationsInNamespaces(IEnumerable<string> implementationNamespaces);
		IPrefixConfiguration AllowImplementationsInNamespace(string implementationNamespace);
	}
}