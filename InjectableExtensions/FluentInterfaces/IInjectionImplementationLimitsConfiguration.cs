namespace Injectable.WS.Extensions
{
	public interface IInjectionImplementationLimitsConfiguration
	{
		IPrefixConfiguration LimitImplementationsToInterfaceNamespace();
		IPrefixConfiguration AllowImplementationsInAnyNamespace();
	}
}