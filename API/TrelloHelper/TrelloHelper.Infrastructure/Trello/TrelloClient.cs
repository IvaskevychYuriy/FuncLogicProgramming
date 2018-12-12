using Infrastructure.Trello;
using Infrastructure.Trello.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrelloHelper.Infrastructure.Trello
{
	// TODO: implement
	public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _httpClient;

        public TrelloClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public Task<Board> AddBoard(Board board)
		{
			throw new System.NotImplementedException();
		}

		public Task<Card> AddCard(List list)
		{
			throw new System.NotImplementedException();
		}

		public Task<List> AddList(List list)
		{
			throw new System.NotImplementedException();
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
		//    var result = await _httpClient.GetAsync("members/${this.userId}/boards/?key=${TRELLO_KEY}&token=${token}").ConfigureAwait(false);

		//    await result.Content.ReadAsStringAsync().ConfigureAwait(false);
		//}
	}
}
