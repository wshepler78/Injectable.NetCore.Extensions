using System;
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
        IPrefixConfiguration AllowImplementationsInAssembliesOf(params Type[] types);
        IPrefixConfiguration AllowImplementationsInAssembliesOf(IEnumerable<Type> types);
        IPrefixConfiguration AllowImplementationsInAssemblyOf(Type type);
        IPrefixConfiguration AllowImplementationsInNamespacesOf(params Type[] types);
        IPrefixConfiguration AllowImplementationsInNamespacesOf(IEnumerable<Type> types);
        IPrefixConfiguration AllowImplementationsInNamespaceOf(Type type);
	}
}