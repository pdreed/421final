namespace _421final.Models
{
    public class PlayerRootTeamAndPlayerVM
    {
        public PlayerRoot player { get; set; }
        public TeamRoot team { get; set; }
        public SeasonAvgRoot stats { get; set; }
        public decimal? fgpct { get; set; }
        public decimal? fg3pct { get; set; }
        public decimal? ftpct { get; set; }

    }
}
