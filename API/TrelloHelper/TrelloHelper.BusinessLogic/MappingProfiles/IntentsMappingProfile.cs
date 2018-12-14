using AutoMapper;
using BusinessLogic.Intent.Enumerations;
using BusinessLogic.Intent.Models;
using BusinessLogic.Query.Models;
using TrelloHelper.BusinessLogic.Tools;

namespace TrelloHelper.BusinessLogic.MappingProfiles
{
	public class IntentsMappingProfile : Profile
	{
		public IntentsMappingProfile()
		{
			// helper mappings
			CreateMap<string, IntentRole?>()
				.ConvertUsing(x => EnumHelper<IntentRole>.ParseDisplayValues(x));


			// maps from Business to Intents models


			// maps from Intents to Business models
			CreateMap<IntentResult, Response>();
            CreateMap<UriIntentResult, Response>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Uri));
		}
	}
}
