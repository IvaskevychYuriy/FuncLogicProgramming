namespace Infrastructure.Trello.Models
{
	public class Board
	{
		public string Id { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }
		public string DateLastActivity { get; set; }
		public string DateLastView { get; set; }
		public string Desc { get; set; }
		public string ShortLink { get; set; }
		public string ShortUrl { get; set; }
	}
}
