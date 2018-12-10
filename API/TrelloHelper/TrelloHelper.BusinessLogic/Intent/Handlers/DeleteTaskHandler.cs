using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using Infrastructure.Trello;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class DeleteTaskHandler : TrelloIntentHandlerBase<DeleteTaskIntent>
	{
		public DeleteTaskHandler(ITrelloClient trelloClient) : base(trelloClient)
		{
		}

		protected override string IntentName => IntentNames.DeleteTask;

		protected override Task<IntentResult> HandleInternal(DeleteTaskIntent intent)
		{
			ValidateName(intent.TaskName, "Task name must be present to delete it");
			ValidateName(intent.ListName, "List name must be present to delete task from it");
			//var model = new DeleteTaskModel()
			//{
			//	TaskName = intent.TaskName
			//};

			//_trelloClient.DeleteTask(model)
			throw new NotImplementedException();
		}
	}
}
