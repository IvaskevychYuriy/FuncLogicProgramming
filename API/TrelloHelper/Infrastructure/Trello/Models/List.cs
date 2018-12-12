using Newtonsoft.Json;

namespace Infrastructure.Trello.Models
{
	public class List
	{
		public string Id { get; set; }
		public string Name { get; set; }

		[JsonProperty("idBoard")]
		public string BoardId { get; set; }
	}
}
