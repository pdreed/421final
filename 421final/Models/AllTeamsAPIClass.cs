using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _421final.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);


    public class AllTeamsMeta
    {
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("next_page")]
        public object NextPage { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class AllTeamsRoot
    {
        [JsonPropertyName("data")]
        public List<TeamRoot> Data { get; set; }

        [JsonPropertyName("meta")]
        public AllTeamsMeta Meta { get; set; }
    }
}
