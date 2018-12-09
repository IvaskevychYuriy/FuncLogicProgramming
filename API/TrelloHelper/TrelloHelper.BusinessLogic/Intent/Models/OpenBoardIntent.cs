using System.Linq;

namespace TrelloHelper.BusinessLogic.Intent.Models
{
	public class OpenBoardIntent : IntentBase
	{
		public string BoardName => Data.Entities.FirstOrDefault()?.Name; 
	}
}
