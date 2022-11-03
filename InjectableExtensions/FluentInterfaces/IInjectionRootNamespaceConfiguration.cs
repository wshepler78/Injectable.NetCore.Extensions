using System;
using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Root Namespace Configuration
	/// </summary>
	public interface IInjectionRootNamespaceConfiguration
	{
		/// <summary>
		/// Looks for Interfaces and implementations in the namespace provided.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		IInjectionImplementationLimitsConfiguration WithRootNamespace(string name);
        /// <summary>
        /// /// Looks for Interfaces and implementations in the namespaces provided.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        IInjectionImplementationLimitsConfiguration WithRootNamespaces(params string[] names);
        /// <summary>
        /// Looks for Interfaces and implementations in the namespaces provided.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        IInjectionImplementationLimitsConfiguration WithRootNamespaces(IEnumerable<string> names);
        /// <summary>
        /// Looks for Interfaces and implementations in the same root namespace as the type provided.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IInjectionImplementationLimitsConfiguration WithRootNamespaceOf(Type type);
        /// <summary>
        /// /// Looks for Interfaces and implementations in the same root namespaces as the types provided.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        IInjectionImplementationLimitsConfiguration WithRootNamespacesOf(params Type[] types);
        /// <summary>
        /// Looks for Interfaces and implementations in the same root namespaces as the types provided.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        IInjectionImplementationLimitsConfiguration WithRootNamespacesOf(IEnumerable<Type> types);
    }
}