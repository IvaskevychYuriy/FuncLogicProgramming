using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Trello
{
    public interface ITrelloUserInfoAccessor
    {
        string UserId { get; }
        string Token { get; }
    }
}
