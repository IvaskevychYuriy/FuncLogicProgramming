using Infrastructure.Trello;
using Infrastructure.Trello.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using TrelloHelper.Infrastructure.Extensions;

namespace TrelloHelper.Infrastructure.Trello
{
	// TODO: implement
	public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _httpClient;
        private readonly TrelloHelperConfiguration _configuration;
        private readonly ITrelloUserInfoAccessor _userInfoAccessor;

        public TrelloClient(
            HttpClient httpClient, 
            TrelloHelperConfiguration configuration, 
            ITrelloUserInfoAccessor userInfoAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userInfoAccessor = userInfoAccessor;
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

        public async Task<List<TrelloBoard>> GetBoards()
        {
            var result = await _httpClient.GetAsync($"members/{_userInfoAccessor.UserId}/boards/?key={_configuration.TrelloApiKey}&token={_userInfoAccessor.Token}");
            return await result.Content.ReadAsJsonAsync<List<TrelloBoard>>();
        }
	}
}
