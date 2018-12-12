using Newtonsoft.Json;

namespace Infrastructure.Trello.Models
{
	public class Card
	{
		public string Id { get; set; }
		public string Name { get; set; }

		[JsonProperty("idList")]
		public string ListId { get; set; }
	}
}
