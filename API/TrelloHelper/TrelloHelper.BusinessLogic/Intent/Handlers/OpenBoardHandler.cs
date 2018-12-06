using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class OpenBoardHandler : TrelloIntentHandlerBase
	{
		public OpenBoardHandler(ITrelloClient trelloClient) : base(trelloClient)
		{
		}

		protected override string IntentName => "OpenBoard";

		protected override async Task<IntentResult> HandleInternal(IntentData intent)
		{
			// TODO: implement
			//var model = new OpenBoardModel()
			//{
			//	BoardName = intent.Entities["BoardName"].Value;
			//}

			//var result = await _trelloClient.OpenBoard(model).ConfigureAwait(false);
			//// TODO: add mapping
			//return new IntentResult();

			throw new NotImplementedException();
		}
	}
}
