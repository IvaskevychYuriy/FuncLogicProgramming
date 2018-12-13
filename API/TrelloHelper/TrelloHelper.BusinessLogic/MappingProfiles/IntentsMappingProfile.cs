using AutoMapper;
using BusinessLogic.Intent.Models;
using BusinessLogic.Query.Models;

namespace TrelloHelper.BusinessLogic.MappingProfiles
{
	public class IntentsMappingProfile : Profile
	{
		public IntentsMappingProfile()
		{
			// maps from Business to Intents models


			// maps from Intents to Business models
			CreateMap<IntentResult, Response>();
            CreateMap<UriIntentResult, Response>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Uri));
		}
	}
}
