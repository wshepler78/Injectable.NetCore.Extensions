using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Injectable.NetCore.Extensions
{
    public static class Injector
    {
        /// <summary>
        /// Path to load assemblies from, if not specified, defaults to AppDomain.CurrentDomain.BaseDirectory
        /// </summary>
        public static string AssemblyDirectoryPath { get; set; }

        /// <summary>
        /// Injects Singleton mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffix">Suffix for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectSingletonsFrom (this IServiceCollection services, string rootNamespace,
            string interfaceSuffix, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectSingletonsFrom(rootNamespace, new List<string> { interfaceSuffix }, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Singleton mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffixList">List of suffixes for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectSingletonsFrom (this IServiceCollection services, string rootNamespace,
            List<string> interfaceSuffixList, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectFrom(InjectionMode.Singleton, rootNamespace, interfaceSuffixList, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Transient mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffix">Suffix for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectTransientsFrom (this IServiceCollection services, string rootNamespace,
            string interfaceSuffix, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectTransientsFrom(rootNamespace, new List<string> { interfaceSuffix }, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Transient mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffixList">List of suffixes for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectTransientsFrom (this IServiceCollection services, string rootNamespace,
            List<string> interfaceSuffixList, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectFrom(InjectionMode.Transient, rootNamespace, interfaceSuffixList, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Scoped mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffix">Suffix for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectScopedFrom (this IServiceCollection services, string rootNamespace,
            string interfaceSuffix, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectScopedFrom(rootNamespace, new List<string> { interfaceSuffix }, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Scoped mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffixList">List of suffixes for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        public static IServiceCollection InjectScopedFrom (this IServiceCollection services, string rootNamespace,
            List<string> interfaceSuffixList, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            return services.InjectFrom(InjectionMode.Scoped, rootNamespace, interfaceSuffixList, interfacePrefix, enforceStrictNaming, restrictImplementationsToInterfaceNamespaces);
        }

        /// <summary>
        /// Injects Singleton, Scoped or Transient mappings based on the supplied parameters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootNamespace">Root namespace to poll for interfaces</param>
        /// <param name="interfaceSuffixList">Suffixes for injectable interfaces</param>
        /// <param name="interfacePrefix">Prefix for injectable interfaces</param>
        /// <param name="enforceStrictNaming">If true, will only inject concrete types whos class names are {interfacePrefix}{ConcreteClassName}{InterfaceSuffix}</param>
        /// <param name="restrictImplementationsToInterfaceNamespaces">If true, will only map interfaces to concrete classes in the same root namespace</param>
        private static IServiceCollection InjectFrom (this IServiceCollection services, InjectionMode mode, string rootNamespace,
            List<string> interfaceSuffixList, string interfacePrefix = "I", bool enforceStrictNaming = true,
            bool restrictImplementationsToInterfaceNamespaces = false)
        {
            var settings = new InjectionSettings
            {
                EnforceStrictNaming = enforceStrictNaming,
                InjectionMode = mode,
                InterfaceRootNamespaces = new List<string> { rootNamespace },
                InterfacePrefix = interfacePrefix,
                InterfaceSuffixList = interfaceSuffixList,
                RestrictImplementationsToInterfaceNamespaces = restrictImplementationsToInterfaceNamespaces
            };

            return services.InjectByConvention(settings);

        }

        /// <summary>
        /// Passes each item in the settingsCollection to InjectByConvention(InjectionSettings) for injection processing
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settingsCollection">List of InjectionSettings to process</param>
        /// <exception cref="InvalidOperationException">Thrown when any of the InjectionSettings fail to validate</exception>
        public static IServiceCollection InjectByConvention (this IServiceCollection services, IEnumerable<IInjectionSettings> settingsCollection)
        {
            foreach (var injectionSettings in settingsCollection)
            {
                services.InjectByConvention(injectionSettings);
            }

            return services;
        }

        /// <summary>
        /// Validates the injection settings, then maps the interfaces matching the conventions in the settings to the implementing classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings">Convention Configuration</param>
        /// <exception cref="InvalidOperationException">Thrown when settings fail validation</exception>
        public static IServiceCollection InjectByConvention (this IServiceCollection services, IInjectionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(AssemblyDirectoryPath))
                AssemblyDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

            var referencedPaths = Directory.GetFiles(AssemblyDirectoryPath, "*.dll").ToList();
            List<TypeInfo> localAssemblyTypes = new List<TypeInfo>();
            List<TypeInfo> localInterfaceTypes = new List<TypeInfo>();

            referencedPaths.ForEach(path =>
            {
                var source = Assembly.LoadFrom(path);

                var localTypeList = source.DefinedTypes.Where(ti => ti.Namespace != null 
                                                            && (!settings.RestrictImplementationsToInterfaceNamespaces
                                                                || settings.InterfaceRootNamespaces.Contains(ti.Namespace.Split('.').First())
                                                            )
                                                        ).ToList();
                localAssemblyTypes.AddRange(localTypeList);

                var interfaceList = source.DefinedTypes.Where(ti => ti.Namespace != null
                                                                    && ti.IsInterface
                                                                    && ti.Name.StartsWith(settings.InterfacePrefix)
                                                                    && settings.InterfaceRootNamespaces.Contains(ti.Namespace.Split('.').First())
                ).ToList();

                localInterfaceTypes.AddRange(interfaceList);
            });

            settings.Validate();

            settings.InterfaceSuffixList.ForEach(suffix =>
            {
                var suffixInterfaces = localInterfaceTypes.Where(ti => ti.Name.StartsWith(settings.InterfacePrefix) && ti.Name.EndsWith(suffix))
                    .Select(ti => ti.AsType()).ToList();

                suffixInterfaces.ForEach(interfaceType =>
                {
                    try
                    {
                        var concreteType = localAssemblyTypes
                            .Where(ti => !ti.IsInterface && (!settings.EnforceStrictNaming || interfaceType.Name == $"{settings.InterfacePrefix}{ti.Name}"))
                            .Select(ti => ti.AsType())
                            .Single(t => t.GetInterfaces().Contains(interfaceType));

                        switch (settings.InjectionMode)
                        {
                            case InjectionMode.Scoped:
                                services.TryAddScoped(interfaceType, concreteType);
                                break;

                            case InjectionMode.Singleton:
                                services.AddSingleton(interfaceType, concreteType);
                                break;

                            case InjectionMode.Transient:
                                services.AddTransient(interfaceType, concreteType);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException($"{settings.InjectionMode} is not a member of InjectionMode");
                        }

                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Write($"{interfaceType.AssemblyQualifiedName} Injection Error : {ex.Message}");
                        throw;
                    }

                });

            });

            return services;
        }

        public static IServiceCollection InjectFromManager(this IServiceCollection services, Action<InjectionConfiguration> configuration)
        {
            var config = new InjectionConfiguration();
            configuration?.Invoke(config);
            config.Manager.InjectServices(services);
            return services;
        }
    }
}

