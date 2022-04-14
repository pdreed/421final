using System.Text.Json.Serialization;

namespace _421final.Models
{
    public class AllPlayersMeta
    {
        [JsonPropertyName("total_pages")]
        public int? TotalPages { get; set; }

        [JsonPropertyName("current_page")]
        public int? CurrentPage { get; set; }

        [JsonPropertyName("next_page")]
        public int? NextPage { get; set; }

        [JsonPropertyName("per_page")]
        public int? PerPage { get; set; }

        [JsonPropertyName("total_count")]
        public int? TotalCount { get; set; }
    }

    public class AllPlayersRoot
    {
        [JsonPropertyName("data")]
        public List<PlayerRoot> Data { get; set; }

        [JsonPropertyName("meta")]
        public AllPlayersMeta Meta { get; set; }
    }
}
