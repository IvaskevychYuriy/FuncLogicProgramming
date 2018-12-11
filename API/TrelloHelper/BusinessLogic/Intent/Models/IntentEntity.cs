using BusinessLogic.Intent.Enumerations;

namespace BusinessLogic.Intent.Models
{
	public class IntentEntity
	{
		public string Name { get; set; }
		
		public IntentRole? Role { get; set; }
	}
}
