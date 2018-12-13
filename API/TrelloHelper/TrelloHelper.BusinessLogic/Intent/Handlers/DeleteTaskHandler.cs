using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using System;
using System.Threading.Tasks;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
    public class DeleteTaskHandler : TrelloIntentHandlerBase<DeleteTaskIntent>
    {
        private readonly IContextProvider _contextProvider;
        private readonly ITrelloTokenProvider _tokenProvider;

        public DeleteTaskHandler(IntentHandlerAggregateService aggregateService,
            IContextProvider contextProvider,
            ITrelloTokenProvider tokenProvider) : base(aggregateService)
        {
            _contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
        }

        protected override string IntentName => IntentNames.DeleteTask;

        protected override async Task<IntentResult> HandleInternal(DeleteTaskIntent intent)
        {
            ValidateName(intent.TaskName, "Task name must be present to delete it");
            ValidateName(intent.ListName, "List name must be present to delete task from it");
            var key = new ContextCacheKeyWrapper
            {
                TrelloToken = _tokenProvider.GetToken()
            };

            var boardId = _contextProvider.Get(key)?.BoardId;
            if (boardId == null)
            {
                throw new InvalidOperationException("Tried to delete a task without an active board");
            }

            var list = await FindListByName(boardId, intent.ListName);

            var task = await FindTaskByName(list, intent.TaskName);

            await _aggregateService.TrelloClient.DeleteCard(task);
            return new IntentResult();
        }
    }
}