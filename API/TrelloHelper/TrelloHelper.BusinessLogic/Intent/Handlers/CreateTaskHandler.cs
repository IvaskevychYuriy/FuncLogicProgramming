using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using Infrastructure.Trello.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class CreateTaskHandler : TrelloIntentHandlerBase<CreateTaskIntent>
	{
        private readonly IContextProvider _contextProvider;
        private readonly ITrelloTokenProvider _tokenProvider;

        public CreateTaskHandler(IntentHandlerAggregateService aggregateService,
            IContextProvider contextProvider,
            ITrelloTokenProvider tokenProvider) : base(aggregateService)
		{
            _contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
        }

		protected override string IntentName => IntentNames.CreateTask;

		protected override async Task<IntentResult> HandleInternal(CreateTaskIntent intent)
		{
			ValidateName(intent.TaskName, "Task name must be present to create it");
			ValidateName(intent.ListName, "List name must be present to create task in it");

            var key = new ContextCacheKeyWrapper
            {
                TrelloToken = _tokenProvider.GetToken()
            };

            var boardId = _contextProvider.Get(key)?.BoardId;
            if (boardId == null)
            {
                throw new InvalidOperationException("Tried to create a task without an active board");
            }

            var list = await FindListByName(boardId, intent.ListName);
            var card = new Card
            {
                ListId = list.Id,
                Name = intent.TaskName
            };
            await _aggregateService.TrelloClient.AddCard(card);
            return new IntentResult();
		}
	}
}
