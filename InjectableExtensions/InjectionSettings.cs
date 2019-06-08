using System;
using System.Collections.Generic;
using System.Linq;
using Injectable.NetCore.Extensions.FluentInterfaces;

namespace Injectable.NetCore.Extensions
{
	public class InjectionSettings : IInjectionSettings, IInjectionModeConfiguration, IInjectionRootNamespaceConfiguration, IInjectionImplementationLimitsConfiguration, IPrefixConfiguration, ISuffixConfiguration, IStrictNamingConfiguration
    {
        /// <summary>
        /// Prefix of interface types, defaults to "I"
        /// </summary>
        public string InterfacePrefix { get; set; } = "I";
        /// <summary>
        /// Interface suffixs to identify injectables
        /// </summary>
        public List<string> InterfaceSuffixList { get; set; } = new List<string>();
        /// <summary>
        /// Namespaces to load interfaces from
        /// </summary>
        public List<string> InterfaceRootNamespaces { get; set; } = new List<string>();
        /// <summary>
        /// When set to true, Injected classes must have the same name as the interface, without the leading "I"
        /// ex: INameProvider will inject the NameProvider class, but not TheNameProvider, even if TheNameProvider implements INameProvider
        /// 
        /// Defaults to true
        /// </summary>
        public bool EnforceStrictNaming { get; set; } = true;
        /// <summary>
        /// When set to true, Only classes in the same root namespaces as the interface collections they implement will be injected
        /// 
        /// Defaults to false
        /// </summary>
        public bool RestrictImplementationsToInterfaceNamespaces { get; set; } = false;

        public InjectionMode InjectionMode { get; set; } = InjectionMode.Scoped;

        /// <summary>
        /// Checks settings for minimum viable usability, throws an InvalidOperationException if validation criteria fails
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting validation fails</exception>
        public void Validate()
        {
            var messages = new List<string>();

            if (InterfaceSuffixList.Count < 1)
                messages.Add("InterfaceSuffixList must contain at least one element");

            if (InterfaceRootNamespaces.Count < 1)
                messages.Add("InterfaceRootNamespaces must contain at least one element");

            if (messages.Count > 0)
                throw new InvalidOperationException(string.Join(Environment.NewLine, messages));
        }

        public IInjectionRootNamespaceConfiguration Configure()
        {
	        return this;
        }

        private static List<string> CleanList(IEnumerable<string> strings)
        {
	        var cleanList = new List<string>();
			strings?.ToList().ForEach(s =>
			{
				if (!string.IsNullOrWhiteSpace(s))
					cleanList.Add(s);
						
			});

			return cleanList;
        }

        IInjectionRootNamespaceConfiguration IInjectionModeConfiguration.WithInjectionMode(InjectionMode mode)
        {
	        InjectionMode = mode;
	        return this;
        }

        public IInjectionImplementationLimitsConfiguration WithRootNamespace(string name)
        {
	        return WithRootNamespaces(name);
        }

        public IInjectionImplementationLimitsConfiguration WithRootNamespaces(params string[] names)
        {
	        return WithRootNamespaces(names.AsEnumerable());
        }

        public IInjectionImplementationLimitsConfiguration WithRootNamespaces(IEnumerable<string> names)
        {
	        InterfaceRootNamespaces = CleanList(names);
	        return this;
        }

        public IPrefixConfiguration LimitImplementationsToInterfaceNamespace()
        {
	        RestrictImplementationsToInterfaceNamespaces = true;
	        return this;
        }

        public IPrefixConfiguration AllowImplementationsInAnyNamespace()
        {
	        RestrictImplementationsToInterfaceNamespaces = false;
	        return this;
        }

        public ISuffixConfiguration WithInterfacePrefix(string prefix)
        {
	        InterfacePrefix = prefix;
	        return this;
        }

        public ISuffixConfiguration WithDefaultInterfacePrefix()
        {
	        return WithInterfacePrefix("I");
        }

        public IStrictNamingConfiguration WithInterfaceSuffixes(IEnumerable<string> suffixes)
        {
	        InterfaceSuffixList = CleanList(suffixes);
			return this;
        }

        public IStrictNamingConfiguration WithInterfaceSuffixes(params string[] suffixes)
        {
	        return WithInterfaceSuffixes(suffixes.AsEnumerable());
        }

        public IStrictNamingConfiguration WithInterfaceSuffix(string suffix)
        {
	        return WithInterfaceSuffixes(suffix);
        }

        public IStrictNamingConfiguration WithoutInterfaceSuffixes()
        {
	        InterfaceSuffixList = new List<string>();
	        return this;
        }

        public IInjectionSettings WithStrictNaming()
        {
	        EnforceStrictNaming = true;
	        return this;
        }

        public IInjectionSettings WithoutStrictNaming()
        {
	        EnforceStrictNaming = false;
	        return this;
        }

        public static IInjectionRootNamespaceConfiguration WithInjectionMode(InjectionMode mode)
        {
			var settings = new InjectionSettings();
			return ((IInjectionModeConfiguration) settings).WithInjectionMode(mode);
        }
    }
}
