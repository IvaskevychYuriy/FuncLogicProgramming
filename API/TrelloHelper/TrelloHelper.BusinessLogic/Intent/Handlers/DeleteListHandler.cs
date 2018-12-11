using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class DeleteListHandler : TrelloIntentHandlerBase<DeleteListIntent>
	{
		public DeleteListHandler(IntentHandlerAggregateService aggregateService) : base(aggregateService)
		{
		}

		protected override string IntentName => IntentNames.DeleteList;

		protected override Task<IntentResult> HandleInternal(DeleteListIntent intent)
		{
			ValidateName(intent.ListName, "List name must be present to delete it");
			//var model = new DeleteListModel()
			//{
			//	ListName = intent.ListName
			//};

			//_trelloClient.DeleteList(model)
			throw new NotImplementedException();
		}
	}
}
