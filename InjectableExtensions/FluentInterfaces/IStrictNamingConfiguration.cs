namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IStrictNamingConfiguration
	{
		IInjectionSettings WithStrictNaming();
		IInjectionSettings WithoutStrictNaming();
	}
}