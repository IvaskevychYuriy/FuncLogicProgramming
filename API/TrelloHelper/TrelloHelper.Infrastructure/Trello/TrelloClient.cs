using Infrastructure.Trello;


namespace TrelloHelper.Infrastructure.Trello
{
    public class TrelloClient : ITrelloClient
    {
        private readonly TrelloHttpClient _httpClient;

        public TrelloClient(TrelloHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void OpenBoard(string id)
        {

            var result = await _httpClient.Client.GetAsync("https://api.trello.com/1/members/${this.userId}/boards/?key=${TRELLO_KEY}&token=${token}");

            await result.Content.ReadAsStringAsync();
        }

    }
}
