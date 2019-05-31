namespace Injectable.WS.Extensions
{
	public interface IInjectionModeConfiguration
	{
		IInjectionRootNamespaceConfiguration WithInjectionMode(InjectionMode mode);
	}
}