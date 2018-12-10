namespace TrelloHelper.BusinessLogic.Intent.Models
{
	public class ListIntentBase : IntentBase
	{
		public string ListName => FirstEntityNameOrDefault; 
	}
}
