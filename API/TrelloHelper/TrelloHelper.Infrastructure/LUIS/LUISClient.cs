using Infrastructure.LUIS;
using Infrastructure.LUIS.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloHelper.Infrastructure.Extensions;

namespace TrelloHelper.Infrastructure.LUIS
{
	public class LUISClient : ILUISClient
    {
        private readonly HttpClient _client;

        public LUISClient(HttpClient client) 
        {
            _client = client;
        }

        public async Task<LUISResponse> GetResponse(LUISRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _client.GetAsync($"&q={request.Query}").ConfigureAwait(false);
			return await result.Content.ReadAsJsonAsync<LUISResponse>();
        }
    }
}
