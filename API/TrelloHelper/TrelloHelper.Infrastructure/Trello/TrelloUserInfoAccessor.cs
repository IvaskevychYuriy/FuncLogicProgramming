using Infrastructure.Trello;
using Microsoft.AspNetCore.Http;

namespace TrelloHelper.Infrastructure.Trello
{
	public class TrelloUserInfoAccessor : ITrelloUserInfoAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TrelloUserInfoAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

		public string UserId => _contextAccessor.HttpContext.Request.Headers["TRELLO_USERID"];

		public string Token => _contextAccessor.HttpContext.Request.Headers["TRELLO_TOKEN"];
	}
}
