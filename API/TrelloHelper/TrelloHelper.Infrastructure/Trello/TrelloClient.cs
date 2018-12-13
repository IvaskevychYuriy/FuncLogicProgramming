using Common.Configurations;
using Infrastructure.Trello;
using Infrastructure.Trello.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloHelper.Infrastructure.Extensions;

namespace TrelloHelper.Infrastructure.Trello
{
	public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _httpClient;
        private readonly TrelloConfig _configuration;
        private readonly ITrelloUserInfoAccessor _userInfoAccessor;

        public TrelloClient(
            HttpClient httpClient,
            IOptions<TrelloConfig> configuration, 
            ITrelloUserInfoAccessor userInfoAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration.Value;
            _userInfoAccessor = userInfoAccessor;
        }
		
		public Task<Board> AddBoard(Board board)
		{
			return Post<Board>($"boards/?name={board.Name}");
		}

		public Task<Card> AddCard(Card card)
		{
			return Post<Card>($"cards/?idList={card.ListId}&name={card.Name}");
		}

		public Task<List> AddList(List list)
		{
			return Post<List>($"lists/?idBoard={list.BoardId}&name={list.Name}");
		}

		public async Task DeleteCard(Card card)
		{
			string url = WithTrelloData($"cards/{card.Id}/");
			var result = await _httpClient.DeleteAsync(url);
			result.EnsureSuccessStatusCode();
		}

		public async Task DeleteList(List list)
		{
			string url = WithTrelloData($"lists/{list.Id}/closed?value=true");
			var result = await _httpClient.PostAsync(url, null);
			result.EnsureSuccessStatusCode();
		}

		public Task<IEnumerable<Board>> GetBoards()
		{
			return Get<IEnumerable<Board>>($"members/{_userInfoAccessor.UserId}/boards/");
		}

		public Task<IEnumerable<Card>> GetCardsForList(List list)
		{
			return Get<IEnumerable<Card>>($"lists/{list.Id}/cards/");
		}

		public Task<IEnumerable<List>> GetListsForBoard(Board board)
		{
			return Get<IEnumerable<List>>($"boards/{board.Id}/lists/");
		}

		public Task<Card> UpdateCard(Card card)
		{
			return Post<Card>($"cards/?idList={card.ListId}&keepFromSource=all");
		}

		private async Task<T> Get<T>(string url)
		{
			url = WithTrelloData(url);
			var result = await _httpClient.GetAsync(url);
			result.EnsureSuccessStatusCode();
			return await result.Content.ReadAsJsonAsync<T>();
		}

		private async Task<T> Post<T>(string url, HttpContent content = null)
		{
			url = WithTrelloData(url);
			var result = await _httpClient.PostAsync(url, content);
			result.EnsureSuccessStatusCode();
			return await result.Content.ReadAsJsonAsync<T>();
		}

		private string WithTrelloData(string url)
		{
			string delim = url.Contains("?") ? "&" : "?";
			return $"{url}{delim}key={_configuration.ApiKey}&token={_userInfoAccessor.Token}";
		}
	}
}
