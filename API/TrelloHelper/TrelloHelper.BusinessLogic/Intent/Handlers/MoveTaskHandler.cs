using System;
using System.Threading.Tasks;
using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class MoveTaskHandler : TrelloIntentHandlerBase<MoveTaskIntent>
	{
        private readonly IContextProvider _contextProvider;
        private readonly ITrelloTokenProvider _tokenProvider;

        public MoveTaskHandler(IntentHandlerAggregateService aggregateService,
            IContextProvider contextProvider,
            ITrelloTokenProvider tokenProvider) : base(aggregateService)
		{
            _contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
        }

		protected override string IntentName => IntentNames.MoveTask;

		protected override async Task<IntentResult> HandleInternal(MoveTaskIntent intent)
		{
			ValidateName(intent.TaskName, "Task name must be present to create it");
			ValidateName(intent.ListName, "List name must be present to move task into it");

            var key = new ContextCacheKeyWrapper
            {
                TrelloToken = _tokenProvider.GetToken()
            };

            var boardId = _contextProvider.Get(key)?.BoardId;
            if (boardId == null)
            {
                throw new InvalidOperationException("Tried to move a task without an active board");
            }

            var list = await FindListByName(boardId, intent.ListName);

            var task = await FindTaskByName(list, intent.TaskName);

            task.ListId = list.Id;
            await _aggregateService.TrelloClient.UpdateCard(task);
            return new IntentResult();
		}
	}
}
