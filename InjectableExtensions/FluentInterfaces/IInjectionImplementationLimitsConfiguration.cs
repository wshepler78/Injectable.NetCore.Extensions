using System;
using System.Collections.Generic;

namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Implementation to Interface Configuration
	/// </summary>
	public interface IInjectionImplementationLimitsConfiguration
	{
		/// <summary>
		/// Limits the implementations to interface namespace.
		/// </summary>
		/// <returns></returns>
		IPrefixConfiguration LimitImplementationsToInterfaceNamespace();
		/// <summary>
		/// Allows the implementations in any namespace.
		/// </summary>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInAnyNamespace();
		/// <summary>
		/// Allows the implementations in namespaces supplied.
		/// </summary>
		/// <param name="implementationNamespaces">The implementation namespaces.</param>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInNamespaces(params string[] implementationNamespaces);
		/// <summary>
		/// Allows the implementations in namespaces supplied.
		/// </summary>
		/// <param name="implementationNamespaces">The implementation namespaces.</param>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInNamespaces(IEnumerable<string> implementationNamespaces);
		/// <summary>
		/// Allows the implementations in namespace supplied.
		/// </summary>
		/// <param name="implementationNamespace">The implementation namespace.</param>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInNamespace(string implementationNamespace);
		/// <summary>
		/// Allows the implementations in assemblies of the supplied types.
		/// </summary>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInAssembliesOf(params Type[] types);
		/// <summary>
		/// Allows the implementations in assemblies of the supplied types.
		/// </summary>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		IPrefixConfiguration AllowImplementationsInAssembliesOf(IEnumerable<Type> types);
        /// <summary>
        /// Allows the implementations in assembly of the supplied type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IPrefixConfiguration AllowImplementationsInAssemblyOf(Type type);
        /// <summary>
        /// Allows the implementations in namespaces of the supplied types.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        IPrefixConfiguration AllowImplementationsInNamespacesOf(params Type[] types);
        /// <summary>
        /// Allows the implementations in namespaces of the supplied types.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        IPrefixConfiguration AllowImplementationsInNamespacesOf(IEnumerable<Type> types);
        /// <summary>
        /// Allows the implementations in namespace of the supplied type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IPrefixConfiguration AllowImplementationsInNamespaceOf(Type type);
	}
}