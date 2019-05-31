namespace Injectable.WS.Extensions
{
	public interface IPrefixConfiguration
	{
		ISuffixConfiguration WithInterfacePrefix(string prefix);
		ISuffixConfiguration WithDefaultInterfacePrefix();
	}
}