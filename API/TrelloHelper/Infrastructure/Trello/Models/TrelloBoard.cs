using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Trello.Models
{
    public class TrelloBoard
    {
        public string DateLastActivity { get; set; }
        public string DateLastView { get; set; }
        public string Desc { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortLink { get; set; }
        public string ShortUrl { get; set; }
        public string Url { get; set; }
    }
}
