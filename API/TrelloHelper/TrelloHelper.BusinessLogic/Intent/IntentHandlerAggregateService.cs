using AutoMapper;
using Infrastructure.Trello;

namespace TrelloHelper.BusinessLogic.Intent
{
	public class IntentHandlerAggregateService
	{
		public ITrelloClient TrelloClient { get; }
		public IMapper Mapper { get; }

		public IntentHandlerAggregateService(
			ITrelloClient trelloClient,
			IMapper mapper)
		{
			TrelloClient = trelloClient;
			Mapper = mapper;
		}
	}
}
