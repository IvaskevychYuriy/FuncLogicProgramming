using Common.Configurations;
using Infrastructure.LUIS;
using Infrastructure.LUIS.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloHelper.Infrastructure.Extensions;

namespace TrelloHelper.Infrastructure.LUIS
{
	public class LUISClient : ILUISClient
    {
        private readonly HttpClient _client;
		private readonly LUISConfig _config;

        public LUISClient(
			HttpClient client,
			IOptions<LUISConfig> config) 
        {
            _client = client;
			_config = config.Value;
        }

        public async Task<LUISResponse> GetResponse(LUISRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _client.GetAsync($"{_config.AppID}?verbose=true&timezoneOffset=-360&subscription-key={_config.SubscriptionKey}&q={request.Query}");
			result.EnsureSuccessStatusCode();
			return await result.Content.ReadAsJsonAsync<LUISResponse>();
        }
    }
}
