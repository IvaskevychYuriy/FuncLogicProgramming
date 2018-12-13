using AutoMapper;
using BusinessLogic.Intent.Models;
using BusinessLogic.Query.Models;
using Infrastructure.LUIS.Models;

namespace TrelloHelper.BusinessLogic.MappingProfiles
{
	public class LUISMappingsProfile : Profile
	{
		public LUISMappingsProfile()
		{
			// maps from Business to Infrastructure models
			CreateMap<Request, LUISRequest>();


			// maps from Infrastructure to Business models
			CreateMap<LUISResponse, IntentData>()
				.ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.TopScoringIntent.Name));
		}
	}
}
