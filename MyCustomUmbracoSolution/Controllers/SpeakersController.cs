using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using MyCustomUmbracoSolution.ApiModels;

namespace MyCustomUmbracoSolution.Controllers
{
	[ApiController]
	[Route("umbraco/api/[controller]")]
	public class SpeakersController : UmbracoApiController
	{
		private readonly IPublishedContentQuery _content;

		public SpeakersController(IPublishedContentQuery content)
		{
			_content = content;
		}
		[HttpGet]
		[Route("speakers")]
		public SpeakersApiModel GetSpeakers()
		{
			var speakersNode = _content.ContentAtRoot().OfType<Speakers>().FirstOrDefault();
			var speakers = new SpeakersApiModel();
			var speakerList = new List<SpeakerApiModel>();
			if (speakersNode != null)
			{
				foreach (var speaker in speakersNode.Children.OfType<Speaker>())
				{
					speakerList.Add(new SpeakerApiModel()
					{
						PersonName = speaker.FullName,
						Name = speaker.Name
					}) ;
				}
				speakers.Name = speakersNode.Name;
				speakers.Title = speakersNode.Title;
				speakers.Speakers = speakerList;
			}

			return speakers;
		}
	}
}
