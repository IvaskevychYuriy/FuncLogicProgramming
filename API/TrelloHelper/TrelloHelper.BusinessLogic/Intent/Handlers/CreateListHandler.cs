using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class CreateListHandler : TrelloIntentHandlerBase<CreateListIntent>
	{
		public CreateListHandler(ITrelloClient trelloClient) : base(trelloClient)
		{
		}

		protected override string IntentName => IntentNames.CreateList;

		protected override Task<IntentResult> HandleInternal(CreateListIntent intent)
		{
			ValidateName(intent.ListName, "List name must be present to create it");
			//var model = new CreateListModel()
			//{
			//	ListName = intent.ListName
			//};

			//_trelloClient.CreateList(model)
			throw new NotImplementedException();
		}
	}
}
