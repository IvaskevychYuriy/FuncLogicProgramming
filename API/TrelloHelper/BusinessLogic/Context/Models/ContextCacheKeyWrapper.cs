using System;

namespace BusinessLogic.Context.Models
{
	public class ContextCacheKeyWrapper
	{
		public string TrelloToken { get; set; }

		public object GetKey()
		{
			// add more properties if needed
			return ValueTuple.Create(TrelloToken);
		}
	}
}
