namespace Infrastructure.Trello
{
	public interface ITrelloUserInfoAccessor
    {
        string UserId { get; }
        string Token { get; }
    }
}
