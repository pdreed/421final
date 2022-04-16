using System.Text.Json.Serialization;

namespace _421final.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class SeasonAvgRoot
    {
        [JsonPropertyName("games_played")]
        public int? GamesPlayed { get; set; }

        [JsonPropertyName("player_id")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("season")]
        public int? Season { get; set; }

        [JsonPropertyName("min")]
        public string? Min { get; set; }

        [JsonPropertyName("fgm")]
        public double? Fgm { get; set; }

        [JsonPropertyName("fga")]
        public double? Fga { get; set; }

        [JsonPropertyName("fg3m")]
        public double? Fg3m { get; set; }

        [JsonPropertyName("fg3a")]
        public double? Fg3a { get; set; }

        [JsonPropertyName("ftm")]
        public double? Ftm { get; set; }

        [JsonPropertyName("fta")]
        public double? Fta { get; set; }

        [JsonPropertyName("oreb")]
        public double? Oreb { get; set; }

        [JsonPropertyName("dreb")]
        public double? Dreb { get; set; }

        [JsonPropertyName("reb")]
        public double? Reb { get; set; }

        [JsonPropertyName("ast")]
        public double? Ast { get; set; }

        [JsonPropertyName("stl")]
        public double? Stl { get; set; }

        [JsonPropertyName("blk")]
        public double? Blk { get; set; }

        [JsonPropertyName("turnover")]
        public double? Turnover { get; set; }

        [JsonPropertyName("pf")]
        public double? Pf { get; set; }

        [JsonPropertyName("pts")]
        public double? Pts { get; set; }

        [JsonPropertyName("fg_pct")]
        public double? FgPct { get; set; }

        [JsonPropertyName("fg3_pct")]
        public double? Fg3Pct { get; set; }

        [JsonPropertyName("ft_pct")]
        public double? FtPct { get; set; }
    }

    public class AllSeasonAvgsRoot
    {
        [JsonPropertyName("data")]
        public List<SeasonAvgRoot> Data { get; set; }
    }


}
