namespace Injectable.WS.Extensions
{
	public interface IStrictNamingConfiguration
	{
		IInjectionSettings WithStrictNaming();
		IInjectionSettings WithoutStrictNaming();
	}
}