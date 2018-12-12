using Infrastructure.Trello;
using Infrastructure.Trello.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloHelper.Infrastructure.Extensions;

namespace TrelloHelper.Infrastructure.Trello
{
	// TODO: implement
	public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _client;
        private readonly ITrelloTokenProvider _tokenProvider;

		public TrelloClient(
			HttpClient client,
			ITrelloTokenProvider tokenProvider)
        {
            _client = client;
			_tokenProvider = tokenProvider;
        }

		public async Task<Board> AddBoard(Board board)
		{
			var result = await _client.PostAsync($"boards/?name={board.Name}", null);
			return await result.Content.ReadAsJsonAsync<Board>();
		}

		public async Task<Card> AddCard(Card card)
		{
			var result = await _client.PostAsync($"cards/?idList={card.ListId}&name={card.Name}", null);
			return await result.Content.ReadAsJsonAsync<Card>();
		}

		public async Task<List> AddList(List list)
		{
			var result = await _client.PostAsync($"lists/?idBoard={list.BoardId}&name={list.Name}", null);
			return await result.Content.ReadAsJsonAsync<List>();
		}

		public Task DeleteList(List list)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Board>> GetBoardsForMember(Member member)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Card>> GetCardsForList(List list)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<List>> GetListsForBoard(Board board)
		{
			throw new System.NotImplementedException();
		}

		public Task<Member> GetMemberByToken()
		{
			throw new System.NotImplementedException();
		}

		public Task OpenBoard(Board board)
		{
			throw new System.NotImplementedException();
		}

		public Task<Card> UpdateCard(Card card)
		{
			throw new System.NotImplementedException();
		}

		//public async Task OpenBoard(string id)
		//{
		//    var result = await _client.GetAsync("members/${this.userId}/boards/?key=${TRELLO_KEY}&token=${token}").ConfigureAwait(false);

		//    await result.Content.ReadAsStringAsync().ConfigureAwait(false);
		//}
	}
}
