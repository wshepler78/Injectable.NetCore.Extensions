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
        }
    }
}
