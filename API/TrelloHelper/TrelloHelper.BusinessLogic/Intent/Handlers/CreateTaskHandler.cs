using System;
using System.Threading.Tasks;
using BusinessLogic.Intent.Models;
using TrelloHelper.BusinessLogic.Intent.Constants;
using TrelloHelper.BusinessLogic.Intent.Models;

namespace TrelloHelper.BusinessLogic.Intent.Handlers
{
	public class CreateTaskHandler : TrelloIntentHandlerBase<CreateTaskIntent>
	{
		public CreateTaskHandler(IntentHandlerAggregateService aggregateService) : base(aggregateService)
		{
		}

		protected override string IntentName => IntentNames.CreateTask;

		protected override Task<IntentResult> HandleInternal(CreateTaskIntent intent)
		{
			ValidateName(intent.TaskName, "Task name must be present to create it");
			ValidateName(intent.ListName, "List name must be present to create task in it");
			//var model = new CreateTaskModel()
			//{
			//	TaskName = intent.TaskName
			//};

			//_trelloClient.CreateTask(model)
			throw new NotImplementedException();
		}
	}
}
