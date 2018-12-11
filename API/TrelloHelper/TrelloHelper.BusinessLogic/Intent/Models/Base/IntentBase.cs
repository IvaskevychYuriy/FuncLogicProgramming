using BusinessLogic.Intent.Enumerations;
using BusinessLogic.Intent.Models;
using System.Linq;

namespace TrelloHelper.BusinessLogic.Intent.Models
{
	public class IntentBase
	{
		public IntentData Data { get; set; }

		protected string FirstEntityNameOrDefault => Data.Entities.FirstOrDefault()?.Name;

		protected string FirstEntityNameByRoleOrDefault (IntentRole role) => Data.Entities.FirstOrDefault(e => e.Role == role)?.Name;
	}
}
