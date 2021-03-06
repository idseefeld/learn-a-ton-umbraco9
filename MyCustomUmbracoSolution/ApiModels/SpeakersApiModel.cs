using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCustomUmbracoSolution.ApiModels
{
	public class SpeakersApiModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("speakers")]
		public IEnumerable<SpeakerApiModel> Speakers { get; set; }
	}
	
}
