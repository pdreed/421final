using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _421final.Models
{
    // TeamRoot myDeserializedClass = JsonSerializer.Deserialize<TeamRoot>(myJsonResponse);
    public class TeamRoot
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("conference")]
        public string? Conference { get; set; }

        [JsonPropertyName("division")]
        public string? Division { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public int? Championships { get; set; }
    }


}
