using Newtonsoft.Json;

namespace Infrastructure.Trello.Models
{
	public class Board
	{
		public string Id { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }
	}
}
