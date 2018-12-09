using Infrastructure.Trello;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrelloHelper.Infrastructure.Trello
{
    public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _httpClient;

        public TrelloClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient Client { get; }

        public async Task OpenBoard(string id)
        {
            var result = await _httpClient.GetAsync("members/${this.userId}/boards/?key=${TRELLO_KEY}&token=${token}").ConfigureAwait(false);

            await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
