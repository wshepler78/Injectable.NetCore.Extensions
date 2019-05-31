namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IInjectionModeConfiguration
	{
		IInjectionRootNamespaceConfiguration WithInjectionMode(InjectionMode mode);
	}
}