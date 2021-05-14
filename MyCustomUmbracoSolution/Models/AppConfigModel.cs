using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomUmbracoSolution.Models
{
	public class AppConfigModel
	{
		public ConnectionStringsModel ConnectionStrings { get; set; }
		public MyCustomSectionModel MyCustomSection { get; set; }
		public IEnumerable<string> AllowedHosts { get; set; }
		public UmbracoConfig Umbraco { get; set; }
	}

	
}
