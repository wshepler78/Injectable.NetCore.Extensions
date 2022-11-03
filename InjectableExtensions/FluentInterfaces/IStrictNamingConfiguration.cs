namespace Injectable.NetCore.Extensions.FluentInterfaces
{
	/// <summary>
	/// Configures naming convention
	/// </summary>
	public interface IStrictNamingConfiguration
	{
		/// <summary>
		/// When strict naming is used, the implementation must match the interface <br /><br />
		/// ISupportingDataService - SupportingDataService &lt;- MATCH <br />
		/// ISupportingDataService - DataService &lt;- NO MATCH
		/// </summary>
		IInjectionSettings WithStrictNaming();
        /// <summary>
        /// When strict naming is not used, the implementation's name does NOT need to match the interface<br /><br />
        /// ISupportingDataService - SupportingDataService &lt;- MATCH <br />
        /// ISupportingDataService - DataService &lt;- MATCH
        /// </summary>
        IInjectionSettings WithoutStrictNaming();
	}
}