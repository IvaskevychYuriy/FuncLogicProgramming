using Newtonsoft.Json;

namespace Infrastructure.LUIS.Models
{
    public class LUISEntity
    {
		[JsonProperty("entity")]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Role { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public double Score { get; set; }
    }
}
