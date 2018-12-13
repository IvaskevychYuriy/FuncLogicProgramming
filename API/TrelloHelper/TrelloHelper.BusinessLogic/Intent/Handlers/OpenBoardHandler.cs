using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class OpenBoardHandler : TrelloIntentHandlerBase<OpenBoardIntent>
	{
		private readonly IContextProvider _contextProvider;
        private readonly ITrelloUserInfoAccessor _tokenProvider;

        public OpenBoardHandler(
			IntentHandlerAggregateService aggregateService,
			IContextProvider contextProvider,
            ITrelloUserInfoAccessor tokenProvider)
			: base(aggregateService)
		{
			_contextProvider = contextProvider;
            _tokenProvider = tokenProvider;
		}

		protected override string IntentName => IntentNames.OpenBoard;

		protected override async Task<IntentResult> HandleInternal(OpenBoardIntent intent)
		{
			ValidateName(intent.BoardName, "Board name must be present to open it");
			
            var boards = await _aggregateService.TrelloClient.GetBoards();
            var board = boards.FirstOrDefault(x => x.Name.Equals(intent.BoardName, StringComparison.InvariantCultureIgnoreCase));

            if (board == null)
            {
                throw new ArgumentException($"Board \"{intent.BoardName}\" does not exist");
            }

			var entry = new ContextCacheEntry
			{
                BoardId = board.Id,
				BoardName = intent.BoardName
			};

			var key = new ContextCacheKeyWrapper
			{
				TrelloToken = _tokenProvider.Token
            };

			_contextProvider.AddOrUpdate(key, entry);
            return new UriIntentResult(board.Url);
		}
	}
}
