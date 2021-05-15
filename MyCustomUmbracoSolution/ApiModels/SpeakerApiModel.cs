using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCustomUmbracoSolution.ApiModels
{
	public class SpeakerApiModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("jobTitle")]
		public string JobJobTitle { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
		[JsonPropertyName("company")]
		public string Company { get; set; }
		[JsonPropertyName("image")]
		public string PictureUrl { get; set; }
	}
}
