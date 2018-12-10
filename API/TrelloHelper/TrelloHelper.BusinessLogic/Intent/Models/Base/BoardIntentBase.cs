namespace TrelloHelper.BusinessLogic.Intent.Models
{
	public class BoardIntentBase : IntentBase
	{
		public string BoardName => FirstEntityNameOrDefault;
	}
}
