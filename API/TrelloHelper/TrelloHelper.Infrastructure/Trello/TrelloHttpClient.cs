using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TrelloHelper.Infrastructure.Trello
{
    public class TrelloHttpClient
    {
        public TrelloHttpClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}
