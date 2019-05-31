namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IInjectionImplementationLimitsConfiguration
	{
		IPrefixConfiguration LimitImplementationsToInterfaceNamespace();
		IPrefixConfiguration AllowImplementationsInAnyNamespace();
	}
}