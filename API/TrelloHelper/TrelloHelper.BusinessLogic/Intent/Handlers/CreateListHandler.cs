using System;
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
	public class CreateListHandler : TrelloIntentHandlerBase<CreateListIntent>
	{
        private readonly IContextProvider _contextProvider;
        private readonly ITrelloUserInfoAccessor _tokenProvider;

        public CreateListHandler(
			IntentHandlerAggregateService aggregateService,
            IContextProvider contextProvider,
            ITrelloUserInfoAccessor tokenProvider) : base(aggregateService)
		{
            _contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
        }

		protected override string IntentName => IntentNames.CreateList;

		protected override async Task<IntentResult> HandleInternal(CreateListIntent intent)
		{
			ValidateName(intent.ListName, "List name must be present to create it");
            var key = new ContextCacheKeyWrapper
            {
                TrelloToken = _tokenProvider.Token
            };

            var boardId = _contextProvider.Get(key)?.BoardId;
            if (boardId == null)
            {
                throw new InvalidOperationException("Tried to create a list without an active board");
            }

            var list = new List
            {
                BoardId = boardId,
                Name = intent.ListName
            };

            await _aggregateService.TrelloClient.AddList(list);
            return new IntentResult();
		}
	}
}
