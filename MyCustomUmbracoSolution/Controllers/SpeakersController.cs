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
		public IEnumerable<SpeakerViewModel> GetSpeakers()
		{
			var speakersNode = _content.ContentAtRoot().OfType<Speakers>().FirstOrDefault();
			var speakerList = new List<SpeakerViewModel>();
			if (speakersNode != null)
			{
				foreach (var speaker in speakersNode.Children.OfType<Speaker>())
				{
					speakerList.Add(new SpeakerViewModel()
					{
						PersonName = speaker.PersonName,
						Name = speaker.Name
					}) ;
				}
			}

			return speakerList;
		}
	}
}
