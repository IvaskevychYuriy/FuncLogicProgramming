using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrelloHelper.Infrastructure.Extensions
{
	public static class HttpContentExtensions
	{
		public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
		{
			string json = await content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
