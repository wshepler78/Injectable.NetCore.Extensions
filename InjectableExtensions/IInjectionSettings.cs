using System;
using System.Collections.Generic;
using Injectable.NetCore.Extensions.FluentInterfaces;

namespace Injectable.NetCore.Extensions
{
	public interface IInjectionSettings
	{
		/// <summary>
		/// Prefix of interface types, defaults to "I"
		/// </summary>
		string InterfacePrefix { get; }

		/// <summary>
		/// Interface suffixs to identify injectables
		/// </summary>
		List<string> InterfaceSuffixList { get; }

		/// <summary>
		/// Namespaces to load interfaces from
		/// </summary>
		List<string> InterfaceRootNamespaces { get; }

		/// <summary>
		/// When set to true, Injected classes must have the same name as the interface, without the leading "I"
		/// ex: INameProvider will inject the NameProvider class, but not TheNameProvider, even if TheNameProvider implements INameProvider
		/// 
		/// Defaults to true
		/// </summary>
		bool EnforceStrictNaming { get; }

		/// <summary>
		/// When set to true, Only classes in the same root namespaces as the interface collections they implement will be injected
		/// 
		/// Defaults to false
		/// </summary>
		bool RestrictImplementationsToInterfaceNamespaces { get; }

		InjectionMode InjectionMode { get; }

		/// <summary>
		/// Checks settings for minimum viable usability, throws an InvalidOperationException if validation criteria fails
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when setting validation fails</exception>
		void Validate();

		/// <summary>
		/// Begins Fluent Configuration
		/// </summary>
        IInjectionRootNamespaceConfiguration Configure();
	}
}