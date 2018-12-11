using System;
using System.Threading.Tasks;
using BusinessLogic.Context;
using BusinessLogic.Context.Models;
using BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class OpenBoardHandler : TrelloIntentHandlerBase<OpenBoardIntent>
	{
		private readonly IContextProvider _contextProvider;

		public OpenBoardHandler(
			IntentHandlerAggregateService aggregateService,
			IContextProvider contextProvider) 
			: base(aggregateService)
		{
			_contextProvider = contextProvider;
		}
		
		protected override string IntentName => IntentNames.OpenBoard;

		protected override async Task<IntentResult> HandleInternal(OpenBoardIntent intent)
		{
			string boardName = intent.BoardName;
			ValidateName(boardName, "Board name must be present to open it");

			// TODO: implement
			// TODO: add mapping
			//var model = new OpenBoardModel()
			//{
			//	BoardName = boardName
			//}

			//var result = await _aggregateService.TrelloClient.OpenBoard(model).ConfigureAwait(false);
			//// TODO: add mapping

			var entry = new ContextCacheEntry()
			{
				BoardName = intent.BoardName
			};

			var key = new ContextCacheKeyWrapper()
			{
				TrelloToken = "" // TODO: get from provider
			};

			_contextProvider.AddOrUpdate(key, entry);

			//return new IntentResult();

			throw new NotImplementedException();
		}
	}
}
