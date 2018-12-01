using System.Collections.Generic;

namespace Infrastructure.LUIS.Models
{
    public class LUISResponse
    {
        public string Query { get; set; }

        public LUISIntent TopScoringIntent { get; set; }

		public List<LUISEntity> Entities { get; set; }
    }
}
