using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class CreateBoardHandler : TrelloIntentHandlerBase<CreateBoardIntent>
	{
		public CreateBoardHandler(IntentHandlerAggregateService aggregateService) : base(aggregateService)
		{
		}

		protected override string IntentName => IntentNames.CreateBoard;

		protected override Task<IntentResult> HandleInternal(CreateBoardIntent intent)
		{
			ValidateName(intent.BoardName, "Board name must be present to create it");
			//var model = new CreateBoardModel()
			//{
			//	BoardName = intent.BoardName
			//};

			//_trelloClient.CreateBoard(model)
			throw new NotImplementedException();
		}
	}
}
