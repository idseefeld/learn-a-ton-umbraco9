using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using MyCustomUmbracoSolution.ApiModels;
using Umbraco.Cms.Core.Services;

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
				//ToDo: Use UmbarcoMapper here?
				foreach (var speaker in speakersNode.Children.OfType<Speaker>().OrderBy(s => s.Name))
				{
					var image = speaker.Picture == null ? null : _content.Media(speaker.Picture.Id) as Image;// ToDo: is this the right way to cast a media type? Wasn't there a more elegant way like TypeMedia?
					speakerList.Add(new SpeakerApiModel()
					{
						Name = speaker.Name,
						Description = speaker.ShortDescription,
						JobJobTitle = speaker.JobTitle,
						Company = speaker.Company,
						PictureUrl = image?.UmbracoFile.Src
					});
				}
				speakers.Name = speakersNode.Name;
				speakers.Description = speakersNode.Description;
				speakers.Speakers = speakerList;
			}

			return speakers;
		}
	}
}
