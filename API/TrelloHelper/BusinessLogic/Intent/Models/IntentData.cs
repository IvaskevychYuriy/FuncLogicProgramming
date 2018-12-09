using System.Collections.Generic;

namespace BusinessLogic.Intent.Models
{
	public class IntentData
    {
		public string Name { get; set; }

		public List<IntentEntity> Entities { get; set; }
	}
}
