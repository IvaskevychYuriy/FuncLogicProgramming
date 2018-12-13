using Newtonsoft.Json;

namespace Infrastructure.LUIS.Models
{
    public class LUISIntent
    {
		[JsonProperty("intent")]
        public string Name { get; set; }

        public double Score { get; set; }
    }
}
