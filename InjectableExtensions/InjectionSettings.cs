using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public bool RestrictImplementationsToInterfaceNamespaces { get; set; }

        /// <summary>
        /// Defines the list of namespaces where implementation of the interfaces is allowed for injection
        ///
        /// An empty or null list will allow implementation in any namespace unless restricted by the RestrictImplementationsToInterfaceNamespaces property
        ///
        /// Namespaces can be provided as the partially (ends with) or fully Qualified Namespace
        /// ex: To inject an implementation from MyAssembly.Utilities.Dates any of the following will work
        ///
        /// "MyAssembly.Utilities.Dates"
        /// "Utilities.Dates"
        /// "Dates"
        /// 
        /// </summary>
        public List<string> AllowedImplementationNamespaces { get; set; } = new List<string>();

        public InjectionMode InjectionMode { get; set; } = InjectionMode.Scoped;

        /// <summary>
        /// When true, all interfaces defined matching the convention must be implemented.
        ///
        /// Defaults to true
        /// </summary>
        public bool ForceImplementationForAllDefinitions { get; set; } = true;

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

        private static IEnumerable<string> GetNamespaces(IEnumerable<Type> types, bool rootOnly)
        {
            var typeList = types.ToList();
            VerifyTypeList(typeList.ToArray());
            if (rootOnly)
            {
                return typeList.Select(t => t.Namespace?.Split('.').First()).Distinct();
            }

            var targetNamespaces = new List<string>();
            var workingList = typeList
                                                .Where(t=> t.Namespace != null)
                                                .OrderBy(t => t.Namespace.Length)
                                                .Select(t => t.Namespace).Distinct();
            foreach (var ns in workingList)
            {
                if (targetNamespaces.Contains(ns))
                {
                    continue;
                }

                var nsParts = ns.Split('.');
                string workingNs = null;

                foreach (var nsPart in nsParts)
                {
                    workingNs = string.IsNullOrWhiteSpace(workingNs) ? nsPart : $"{workingNs}.{nsPart}";

                    if (ns == workingNs)
                    {
                        targetNamespaces.Add(workingNs);
                    }

                    if (targetNamespaces.Contains(workingNs))
                    {
                        break;
                    }
                }
            }

            return targetNamespaces;
        }

        private static void VerifyTypeList(Type[] types)
        {
            if (types.Length < 1 || types.All(t => t == null))
            {
                throw new ArgumentOutOfRangeException(nameof(types), "At least one type must be provided");
            }
        }

        public IInjectionRootNamespaceConfiguration Configure(bool forceImplementationForAllDefinitions)
        {
            ForceImplementationForAllDefinitions = forceImplementationForAllDefinitions;
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

        private static void VerifyType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Type cannot be null");
            }
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

        public IInjectionImplementationLimitsConfiguration WithRootNamespaceOf(Type type) => WithRootNamespaces(GetNamespaces(new List<Type> { type }, true));

        public IInjectionImplementationLimitsConfiguration WithRootNamespacesOf(params Type[] types) => WithRootNamespaces(GetNamespaces(types, true));

        public IInjectionImplementationLimitsConfiguration WithRootNamespacesOf(IEnumerable<Type> types) =>
            WithRootNamespacesOf(types.ToArray());

        public IInjectionImplementationLimitsConfiguration InNamespaceOf(Type type) => WithRootNamespaces(GetNamespaces(new List<Type>{type}, false));

        public IInjectionImplementationLimitsConfiguration InNamespacesOf(params Type[] types) => WithRootNamespaces(GetNamespaces(types, false));

        public IInjectionImplementationLimitsConfiguration InNamespacesOf(IEnumerable<Type> types) => WithRootNamespaces(GetNamespaces(types, false));

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

        public IPrefixConfiguration AllowImplementationsInNamespaces(params string[] implementationNamespaces)
        {
            return AllowImplementationsInNamespaces(implementationNamespaces.AsEnumerable());
        }

        public IPrefixConfiguration AllowImplementationsInNamespaces(IEnumerable<string> implementationNamespaces)
        {
            RestrictImplementationsToInterfaceNamespaces = false;
            AllowedImplementationNamespaces = implementationNamespaces.ToList();
            return this;
        }

        public IPrefixConfiguration AllowImplementationsInNamespace(string implementationNamespace)
        {
            return AllowImplementationsInNamespaces(implementationNamespace);
        }

        public IPrefixConfiguration AllowImplementationsInAssembliesOf(params Type[] types) => AllowImplementationsInNamespaces(GetNamespaces(types, true));

        public IPrefixConfiguration AllowImplementationsInAssembliesOf(IEnumerable<Type> types) =>
            AllowImplementationsInAssembliesOf(types.ToArray());

        public IPrefixConfiguration AllowImplementationsInAssemblyOf(Type type) =>
            AllowImplementationsInAssembliesOf(new List<Type> { type });

        public IPrefixConfiguration AllowImplementationsInNamespacesOf(params Type[] types) => AllowImplementationsInNamespaces(GetNamespaces(types, false));

        public IPrefixConfiguration AllowImplementationsInNamespacesOf(IEnumerable<Type> types) => AllowImplementationsInNamespaces(GetNamespaces(types, false));

        public IPrefixConfiguration AllowImplementationsInNamespaceOf(Type type) => AllowImplementationsInNamespaces(GetNamespaces(new List<Type> { type }, false));

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
            return ((IInjectionModeConfiguration)settings).WithInjectionMode(mode);
        }
    }
}
