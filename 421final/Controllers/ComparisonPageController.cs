using _421final.Data;
using _421final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace _421final.Views
{
    public class ComparisonPageController : Controller
    {
        private ApplicationDbContext _context;

        public ComparisonPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ComparisonPage vm = new ComparisonPage();
            List<PlayerRoot> temp = new List<PlayerRoot>();
            List<PlayerRoot> temp2 = new List<PlayerRoot>();
            var dbLebron = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 237);
            temp.Add(dbLebron);
            var dbKD = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 140);
            temp.Add(dbKD);
            var dbHarden = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 192);
            temp.Add(dbHarden);
            var dbSteph = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 115);
            temp2.Add(dbSteph);
            var dbJoel = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 145);
            temp2.Add(dbJoel);
            var dbBooker = await _context.PlayerRoot.FirstOrDefaultAsync(m => m.Id == 57);
            temp2.Add(dbBooker);
            vm.playerList1 = temp;
            vm.playerList2 = temp2;
            vm.playerListHeader = "Suggested players";

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(string textBoxValue)
        {
            
            bool searchForStats1 = false;
            bool searchForStats2 = false;
            bool retrieveStats1 = false;
            bool retrieveStats2 = false;
            bool badInput = false;
            ComparisonPage vm = new ComparisonPage();
            vm.errorMsg1 = "";
            vm.errorMsg2 = "";
            vm.searchErrorMsg = "";
            vm.playerListHeader = "Results";
            string[] words = textBoxValue.Split(", ");
            string[]? firstSearch = null;
            string[]? secondSearch = null;
            if (words.Length == 2)
            {
                firstSearch = words[0].Split(" ");
                secondSearch = words[1].Split(" ");
            }
            else
            {
                //firstSearch[0] = "Error";
                //secondSearch[0] = "Error";
                badInput = true;
                vm.searchErrorMsg = "Incorrect Format";
            }
            if (badInput == false)
            {
                vm.searchName1 = firstSearch[0] + " " + firstSearch[1];
                string web = "https://www.balldontlie.io/api/v1/players?per_page=100&page_num=1&search=";
                string address = web + vm.searchName1;
                WebRequest request = WebRequest.Create(address);
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string jsonString = reader.ReadToEnd();
                    AllPlayersRoot? playerResults = JsonSerializer.Deserialize<AllPlayersRoot>(jsonString);
                    if (playerResults.Data.Count == 1)
                    {
                        vm.playerList1 = playerResults.Data;
                        searchForStats1 = true;
                    }

                }

                if (searchForStats1 == true)
                {
                    string webStats = "https://www.balldontlie.io/api/v1/season_averages?season=";
                    string webStats2 = "&player_ids[]=";
                    string addressStats = webStats + firstSearch[2] + webStats2 + vm.playerList1[0].Id.ToString();
                    vm.year1 = firstSearch[2];
                    WebRequest requestStats = WebRequest.Create(addressStats);
                    WebResponse responseStats = requestStats.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)responseStats).StatusDescription);

                    // Get the stream containing content returned by the server.
                    // The using block ensures the stream is automatically closed.
                    using (Stream dataStream = responseStats.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string jsonString = reader.ReadToEnd();
                        AllSeasonAvgsRoot? playerStats = JsonSerializer.Deserialize<AllSeasonAvgsRoot>(jsonString);
                        if (playerStats.Data.Count == 1)
                        {
                            vm.player1Stats = playerStats.Data[0];
                            retrieveStats1 = true;
                        }
                    }

                    if (retrieveStats1 == true)
                    {
                        vm.player1Stats.FgPct = vm.player1Stats.FgPct * 100;
                        vm.player1Stats.Fg3Pct = vm.player1Stats.Fg3Pct * 100;
                        vm.player1Stats.FtPct = vm.player1Stats.FtPct * 100;

                        vm.fgpct1 = (decimal?)vm.player1Stats.FgPct;
                        vm.fg3pct1 = (decimal?)vm.player1Stats.Fg3Pct;
                        vm.ftpct1 = (decimal?)vm.player1Stats.FtPct;
                    }
                    else
                    {
                        SeasonAvgRoot tempAvg = new SeasonAvgRoot();
                        tempAvg.Ast = 0;
                        tempAvg.Blk = 0;
                        tempAvg.Pts = 0;
                        tempAvg.Pf = 0;
                        tempAvg.Reb = 0;
                        tempAvg.Fg3Pct = 0;
                        tempAvg.FgPct = 0;
                        tempAvg.FtPct = 0;
                        tempAvg.Turnover = 0;
                        tempAvg.GamesPlayed = 0;
                        tempAvg.Min = "0";
                        tempAvg.Stl = 0;
                        vm.player1Stats = tempAvg;
                        vm.fgpct1 = 0;
                        vm.fg3pct1 = 0;
                        vm.ftpct1 = 0;
                        vm.errorMsg1 = firstSearch[0] + " " + firstSearch[1] + " did not participate during the entered year.";
                    }

                }
                else
                {
                    PlayerRoot Nobody = new PlayerRoot();
                    Nobody.Id = 1000000;
                    Nobody.FirstName = "N/A";
                    Nobody.LastName = "N/A";
                    Nobody.Position = "N/A";
                    Nobody.HeightFeet = 0;
                    Nobody.HeightInches = 0;
                    Nobody.WeightPounds = 0;
                    List<PlayerRoot> tempList = new List<PlayerRoot>();
                    tempList.Add(Nobody);
                    vm.playerList1 = tempList;
                    SeasonAvgRoot tempAvg = new SeasonAvgRoot();
                    tempAvg.Ast = 0;
                    tempAvg.Blk = 0;
                    tempAvg.Pts = 0;
                    tempAvg.Pf = 0;
                    tempAvg.Reb = 0;
                    tempAvg.Fg3Pct = 0;
                    tempAvg.FgPct = 0;
                    tempAvg.FtPct = 0;
                    tempAvg.Turnover = 0;
                    tempAvg.GamesPlayed = 0;
                    tempAvg.Min = "0";
                    tempAvg.Stl = 0;
                    vm.player1Stats = tempAvg;
                    vm.fgpct1 = 0;
                    vm.fg3pct1 = 0;
                    vm.ftpct1 = 0;
                    vm.errorMsg1 = "Player Not Found";
                }




                vm.searchName2 = secondSearch[0] + " " + secondSearch[1];
                string web2 = "https://www.balldontlie.io/api/v1/players?per_page=100&page_num=1&search=";
                string address2 = web2 + vm.searchName2;
                WebRequest request2 = WebRequest.Create(address2);
                WebResponse response2 = request2.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response2).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (Stream dataStream = response2.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string jsonString = reader.ReadToEnd();
                    AllPlayersRoot? playerResults = JsonSerializer.Deserialize<AllPlayersRoot>(jsonString);
                    if (playerResults.Data.Count == 1)
                    {
                        vm.playerList2 = playerResults.Data;
                        searchForStats2 = true;
                    }
                }


                if (searchForStats2 == true)
                {
                    string webStatsPlayer2 = "https://www.balldontlie.io/api/v1/season_averages?season=";
                    string webStatsPlayer2b = "&player_ids[]=";
                    string addressStats2 = webStatsPlayer2 + secondSearch[2] + webStatsPlayer2b + vm.playerList2[0].Id.ToString();
                    vm.year2 = secondSearch[2];
                    WebRequest requestStats2 = WebRequest.Create(addressStats2);
                    WebResponse responseStats2 = requestStats2.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)responseStats2).StatusDescription);

                    // Get the stream containing content returned by the server.
                    // The using block ensures the stream is automatically closed.
                    using (Stream dataStream = responseStats2.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string jsonString = reader.ReadToEnd();
                        AllSeasonAvgsRoot? playerStats = JsonSerializer.Deserialize<AllSeasonAvgsRoot>(jsonString);
                        if (playerStats.Data.Count == 1)
                        {
                            vm.player2Stats = playerStats.Data[0];
                            retrieveStats2 = true;
                        }
                    }
                    if (retrieveStats2 == true)
                    {
                        vm.player2Stats.FgPct = vm.player2Stats.FgPct * 100;
                        vm.player2Stats.Fg3Pct = vm.player2Stats.Fg3Pct * 100;
                        vm.player2Stats.FtPct = vm.player2Stats.FtPct * 100;

                        vm.fgpct2 = (decimal?)vm.player2Stats.FgPct;
                        vm.fg3pct2 = (decimal?)vm.player2Stats.Fg3Pct;
                        vm.ftpct2 = (decimal?)vm.player2Stats.FtPct;
                    }
                    else
                    {
                        SeasonAvgRoot tempAvg = new SeasonAvgRoot();
                        tempAvg.Ast = 0;
                        tempAvg.Blk = 0;
                        tempAvg.Pts = 0;
                        tempAvg.Pf = 0;
                        tempAvg.Reb = 0;
                        tempAvg.Fg3Pct = 0;
                        tempAvg.FgPct = 0;
                        tempAvg.FtPct = 0;
                        tempAvg.Turnover = 0;
                        tempAvg.GamesPlayed = 0;
                        tempAvg.Min = "0";
                        tempAvg.Stl = 0;
                        vm.fgpct2 = 0;
                        vm.fg3pct2 = 0;
                        vm.ftpct2 = 0;
                        vm.player2Stats = tempAvg;
                        vm.errorMsg2 = secondSearch[0] + " " + secondSearch[1] + " did not participate during the entered year.";
                    }
                }
                else
                {
                    PlayerRoot Nobody = new PlayerRoot();
                    Nobody.Id = 1000000;
                    Nobody.FirstName = "N/A";
                    Nobody.LastName = "N/A";
                    Nobody.Position = "N/A";
                    Nobody.HeightFeet = 0;
                    Nobody.HeightInches = 0;
                    Nobody.WeightPounds = 0;
                    List<PlayerRoot> tempList = new List<PlayerRoot>();
                    tempList.Add(Nobody);
                    vm.playerList2 = tempList;
                    SeasonAvgRoot tempAvg = new SeasonAvgRoot();
                    tempAvg.Ast = 0;
                    tempAvg.Blk = 0;
                    tempAvg.Pts = 0;
                    tempAvg.Pf = 0;
                    tempAvg.Reb = 0;
                    tempAvg.Fg3Pct = 0;
                    tempAvg.FgPct = 0;
                    tempAvg.FtPct = 0;
                    tempAvg.Turnover = 0;
                    tempAvg.GamesPlayed = 0;
                    tempAvg.Min = "0";
                    tempAvg.Stl = 0;
                    vm.fgpct2 = 0;
                    vm.fg3pct2 = 0;
                    vm.ftpct2 = 0;
                    vm.player2Stats = tempAvg;
                    vm.errorMsg2 = "Player Not Found";
                }
            }
            else
            {
                PlayerRoot Nobody = new PlayerRoot();
                Nobody.Id = 1000000;
                Nobody.FirstName = "N/A";
                Nobody.LastName = "N/A";
                Nobody.Position = "N/A";
                Nobody.HeightFeet = 0;
                Nobody.HeightInches = 0;
                Nobody.WeightPounds = 0;
                List<PlayerRoot> tempList = new List<PlayerRoot>();
                tempList.Add(Nobody);
                vm.playerList1 = tempList;
                vm.playerList2 = tempList;
            }

            return View(vm);
        }
    }
}
