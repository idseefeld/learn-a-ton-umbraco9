using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCustomUmbracoSolution.ApiModels
{
	public class SpeakersViewModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("speakers")]
		public IEnumerable<SpeakerViewModel> Speakers { get; set; }
	}
	public class SpeakerViewModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("fullName")]
		public string PersonName { get; set; }

	}
}
