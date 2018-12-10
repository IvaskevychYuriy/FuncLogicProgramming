using BusinessLogic.Intent.Enumerations;

namespace TrelloHelper.BusinessLogic.Intent.Models
{
	public class TaskIntentBase : IntentBase
	{
		public string TaskName => FirstEntityNameByRoleOrDefault(IntentRole.Task);

		public string ListName => FirstEntityNameByRoleOrDefault(IntentRole.List);
	}
}
