namespace _421final.Models
{
    public class ComparisonPage
    {
        public string? searchName1 { get; set; }
        public string? searchName2 { get; set; }
        public List<PlayerRoot>? playerList1 { get; set; }
        public List<PlayerRoot>? playerList2 { get; set; }
        public SeasonAvgRoot? player1Stats { get; set; }
        public SeasonAvgRoot? player2Stats { get; set; }
        public decimal? fgpct1 { get; set; }
        public decimal? fg3pct1 { get; set; }
        public decimal? ftpct1 { get; set; }
        public decimal? fgpct2 { get; set; }
        public decimal? fg3pct2 { get; set; }
        public decimal? ftpct2 { get; set; }
        public string? searchErrorMsg { get; set; }
        public string? errorMsg1 { get; set; }
        public string? errorMsg2 { get; set; }
    }
}
