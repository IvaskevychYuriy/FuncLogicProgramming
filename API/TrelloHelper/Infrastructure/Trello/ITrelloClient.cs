using Infrastructure.Trello.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Trello
{
    public interface ITrelloClient
    {
		Task<Member> GetMemberByToken();


		Task<IEnumerable<Board>> GetBoardsForMember(Member member);

		Task OpenBoard(Board board);

		Task<Board> AddBoard(Board board);

		
		Task<IEnumerable<List>> GetListsForBoard(Board board);

		Task<List> AddList(List list);

		Task DeleteList(List list);


		Task<IEnumerable<Card>> GetCardsForList(List list);

		Task<Card> AddCard(Card card);

		Task<Card> UpdateCard(Card card);
        Task<List<TrelloBoard>> GetBoards();
    }
}
