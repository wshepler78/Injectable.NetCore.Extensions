using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Injectable.NetCore.Extensions;
using Xunit;

namespace Tests.InjectableExtensions
{
    public class FluentUnitTests
    {
        public const string TestObjectRootNamespace = "TestObjects";
        public const string TestObjectRootNamespace2 = "TestObjects_2";

        public static IEnumerable<object[]> GetRootNamespaceTestData()
        {
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(1)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(2)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace, TestObjectRootNamespace2},
                2,
                AllTypes
            };
        }

        public static IEnumerable<object[]> GetExplicitNamespaceTestData()
        {
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(1)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(2)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes
            };
            yield return new object[]
            {
                AllTypes.Select(t=>t.Namespace).ToList(),
                2,
                AllTypes
            };
        }

        public static IEnumerable<object[]> GetNamespaceTestData()
        {
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(1)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes.Take(2)
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace},
                1,
                TestObjectsTypes
            };
            yield return new object[]
            {
                new List<string> {TestObjectRootNamespace, TestObjectRootNamespace2},
                2,
                AllTypes
            };
            yield return new object[]
            {
                new List<string> {typeof(TestObjects.Level2_1.TestObject2_1).Namespace, typeof(TestObjects.Level2_2.TestObject2_2).Namespace},
                2,
                new List<Type>{typeof(TestObjects.Level2_1.TestObject2_1), typeof(TestObjects.Level2_2.TestObject2_2) }
            };
            yield return new object[]
            {
                new List<string> {typeof(TestObjects.Level2_1.TestObject2_1).Namespace, typeof(TestObjects.Level2_2.TestObject2_2).Namespace},
                2,
                new List<Type>{typeof(TestObjects.Level2_1.TestObject2_1), typeof(TestObjects.Level2_2.TestObject2_2), typeof(TestObjects.Level2_2.TestObject2_3) }
            };

        }

        public static List<Type> AllTypes  {
            get
            {
                var types = new List<Type>();
                types.AddRange(TestObjectsTypes);
                types.AddRange(TestObjects2Types);
                return types;
            }
        }

        public static List<Type> TestObjectsTypes => new List<Type>
        {
            typeof(TestObjects.TestObject1), typeof(TestObjects.Level2_1.TestObject2_1),
            typeof(TestObjects.Level2_2.TestObject2_2)
        };

        public static List<Type> TestObjects2Types => new List<Type>
        {
            typeof(TestObjects_2.TestObject1), typeof(TestObjects_2.Level2_1.TestObject2_1),
            typeof(TestObjects_2.Level2_2.TestObject2_2)
        };

        [Fact]
        public void TestFluentCreation()
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespace("Someroot")
                .LimitImplementationsToInterfaceNamespace()
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.True(settings.InterfaceRootNamespaces.Count > 0);

            var settings2 = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespaces("Someroot", "someOtherRoot")
                .LimitImplementationsToInterfaceNamespace()
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.True(settings2.InterfaceRootNamespaces.Count > 1);
        }

        [Fact]
        public void WhenObjectIsSpecified_AsRootNamespaceProvider_ThenTheNamespaceIsAddedToTheList()
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespaceOf(typeof(TestObjects.TestObject1))
                .LimitImplementationsToInterfaceNamespace()
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.True(settings.InterfaceRootNamespaces.Count == 1);
            Assert.Equal(TestObjectRootNamespace, settings.InterfaceRootNamespaces?.First());
        }

        [Theory]
        [MemberData(nameof(GetRootNamespaceTestData), DisableDiscoveryEnumeration = true)]
        public void WhenObjectsAreSpecified_AsRootNamespaceProviders_ThenTheNamespaceIsAddedToTheListTheExpectedNumberOfTimes(List<string> rootNamespaces, int expectedCount, IEnumerable<Type> types)
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespacesOf(types)
                .LimitImplementationsToInterfaceNamespace()
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.Equal(expectedCount, settings.InterfaceRootNamespaces.Count);
            Assert.True(settings.InterfaceRootNamespaces.All(rootNamespaces.Contains));
        }

        [Fact]
        public void WhenObjectIsSpecified_AsImplementationNamespaceProvider_ThenTheNamespaceIsAddedToTheList()
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespaceOf(typeof(TestObjects.TestObject1))
                .AllowImplementationsInAssemblyOf(typeof(TestObjects.Level2_2.TestObject2_2))
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.True(settings.InterfaceRootNamespaces.Count == 1);
            Assert.Equal(TestObjectRootNamespace, settings.AllowedImplementationNamespaces?.First());
        }

        [Theory]
        [MemberData(nameof(GetRootNamespaceTestData), DisableDiscoveryEnumeration = true)]
        public void WhenObjectsAreSpecified_AsImplementationNamespaceProviders_ThenTheNamespaceIsAddedToTheImplementationNamespaceListExpectedNumberOfTimes(List<string> rootNamespaces, int expectedCount, IEnumerable<Type> types)
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespacesOf(typeof(TestObjects.TestObject1))
                .AllowImplementationsInAssembliesOf(types)
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.Equal(expectedCount, settings.AllowedImplementationNamespaces.Count);
            Assert.True(settings.AllowedImplementationNamespaces.All(rootNamespaces.Contains));
        }

        [Theory]
        [MemberData(nameof(GetExplicitNamespaceTestData), DisableDiscoveryEnumeration = true)]
        public void WhenObjectsAreSpecified_AsExplicitImplementationNamespaceProviders_ThenTheNamespaceIsAddedToTheImplementationNamespaceListExpectedNumberOfTimes(List<string> rootNamespaces, int expectedCount, IEnumerable<Type> types)
        {
            var settings = InjectionSettings
                .WithInjectionMode(InjectionMode.Scoped)
                .WithRootNamespacesOf(typeof(TestObjects.TestObject1))
                .AllowImplementationsInNamespacesOf(types)
                .WithInterfacePrefix("I")
                .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
                .WithStrictNaming();

            Assert.Equal(expectedCount, settings.AllowedImplementationNamespaces.Count);
            Assert.True(settings.AllowedImplementationNamespaces.All(rootNamespaces.Contains));
        }
    }
}
