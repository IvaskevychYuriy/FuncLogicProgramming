using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class MoveTaskHandler : TrelloIntentHandlerBase<MoveTaskIntent>
	{
		public MoveTaskHandler(IntentHandlerAggregateService aggregateService) : base(aggregateService)
		{
		}

		protected override string IntentName => IntentNames.MoveTask;

		protected override Task<IntentResult> HandleInternal(MoveTaskIntent intent)
		{
			ValidateName(intent.TaskName, "Task name must be present to create it");
			ValidateName(intent.ListName, "List name must be present to move task into it");
			//var model = new MoveTaskModel()
			//{
			//	TaskName = intent.TaskName
			//};

			//_trelloClient.MoveTask(model)
			throw new NotImplementedException();
		}
	}
}
