namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	public interface IPrefixConfiguration
	{
		ISuffixConfiguration WithInterfacePrefix(string prefix);
		ISuffixConfiguration WithDefaultInterfacePrefix();
	}
}