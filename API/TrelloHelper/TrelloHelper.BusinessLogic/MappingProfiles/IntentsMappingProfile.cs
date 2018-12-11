using AutoMapper;
using BusinessLogic.Intent.Models;
using BusinessLogic.Query.Models;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.MappingProfiles
{
	public class IntentsMappingProfile : Profile
	{
		public IntentsMappingProfile()
		{
			// maps from Business to Intents models
			CreateMap<IntentData, IntentBase>()
				.IncludeAllDerived()
				.ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));


			// maps from Intents to Business models
			CreateMap<IntentResult, Response>();
		}
	}
}
