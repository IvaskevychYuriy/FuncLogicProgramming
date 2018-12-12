using Infrastructure.Trello;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloHelper.Infrastructure.Trello
{
    public class TrelloUserInfoAccessor: ITrelloUserInfoAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        TrelloUserInfoAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext.Request.Headers["TRELLO_USERID"];
            }
        }

        public string Token
        {
            get
            {
                return _contextAccessor.HttpContext.Request.Headers["TRELLO_TOKEN"];
            }
        }
    }
}
