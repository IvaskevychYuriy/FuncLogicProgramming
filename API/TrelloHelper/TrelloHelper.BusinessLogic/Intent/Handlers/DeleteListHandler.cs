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
	public class DeleteListHandler : TrelloIntentHandlerBase<DeleteListIntent>
	{
        private readonly IContextProvider _contextProvider;
        private readonly ITrelloTokenProvider _tokenProvider;

        public DeleteListHandler(IntentHandlerAggregateService aggregateService,
            IContextProvider contextProvider,
            ITrelloTokenProvider tokenProvider) : base(aggregateService)
		{
            _contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
        }

		protected override string IntentName => IntentNames.DeleteList;

		protected override async Task<IntentResult> HandleInternal(DeleteListIntent intent)
		{
			ValidateName(intent.ListName, "List name must be present to delete it");
            var key = new ContextCacheKeyWrapper
            {
                TrelloToken = _tokenProvider.GetToken()
            };

            var boardId = _contextProvider.Get(key)?.BoardId;
            if (boardId == null)
            {
                throw new InvalidOperationException("Tried to delete a list without an active board");
            }

            var list = await FindListByName(boardId, intent.ListName);
            await _aggregateService.TrelloClient.DeleteList(list);
            return new IntentResult();
		}
	}
}
