using System.Collections.Generic;
using Injectable.NetCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.InjectableExtensions
{
    [TestClass]
    public class FluentUnitTests
    {
        [TestMethod]
        public void TestFluentCreation()
        {
	        var settings = InjectionSettings
		        .WithInjectionMode(InjectionMode.Scoped)
		        .WithRootNamespace("Someroot")
		        .LimitImplementationsToInterfaceNamespace()
		        .WithInterfacePrefix("I")
		        .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
		        .WithStrictNaming();

	        Assert.IsTrue(settings.InterfaceRootNamespaces.Count > 0);

			var settings2 = InjectionSettings
		        .WithInjectionMode(InjectionMode.Scoped)
		        .WithRootNamespaces("Someroot", "someOtherRoot")
		        .LimitImplementationsToInterfaceNamespace()
		        .WithInterfacePrefix("I")
		        .WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
		        .WithStrictNaming();

			var settings3 = InjectionSettings
				.WithInjectionMode(InjectionMode.Scoped)
				.WithRootNamespaces(new List<string>{"Someroot", "someOtherRoot"})
				.LimitImplementationsToInterfaceNamespace()
				.WithInterfacePrefix("I")
				.WithInterfaceSuffixes(new List<string> { "Repository", "Service" })
				.WithStrictNaming();
        }
    }
}
