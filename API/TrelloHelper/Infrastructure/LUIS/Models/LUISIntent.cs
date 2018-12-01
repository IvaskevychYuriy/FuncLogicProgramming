using Newtonsoft.Json;

namespace Infrastructure.LUIS.Models
{
    public class LUISIntent
    {
		[JsonProperty("intent")]
        public string Name { get; set; }

        public int Score { get; set; }
    }
}
