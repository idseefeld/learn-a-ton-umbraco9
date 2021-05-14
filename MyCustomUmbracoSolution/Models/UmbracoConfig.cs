using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomUmbracoSolution.Models
{
	public class UmbracoConfig
	{
		public UmbracoCmsConfig Cms { get; set; }
	}
	public class UmbracoCmsConfig
	{
		public UmbracoCmsGlobalConfig Global { get; set; }
	}
	public class UmbracoCmsGlobalConfig
	{
		public UmbracoCmsGlobalSmtpConfig Smtp { get; set; }
	}
	public class UmbracoCmsGlobalSmtpConfig
	{
		public string From { get; set; }
		public string Host { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int Port { get; set; }
		public bool EnableSsl { get; set; }
	}
}
