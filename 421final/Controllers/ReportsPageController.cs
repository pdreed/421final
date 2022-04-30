using _421final.Data;
using _421final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;


namespace _421final.Controllers
{
    public class ReportsPageController : Controller
    {
        private ApplicationDbContext _context;

        public ReportsPageController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = SD.User + "," + SD.Admin)]
        public async Task<IActionResult> Index()
        {
            ReportsPage vm = new ReportsPage();
            vm.playerList = await _context.PlayerRoot.ToListAsync();
            vm.teamList = await _context.TeamRoot.ToListAsync();
            vm.NoChampionList = NonChampionshipList(vm.teamList);
            vm.ChampionList = ChampionshipList(vm.teamList);
            vm.WeightList = WeightList(vm.playerList);
            vm.HeightList = HeightList(vm.playerList);


            vm.ChampionList.OrderBy(x => x.Championships).ToList();
            return View(vm);

        }

        private static List<TeamRoot> ChampionshipList(List<TeamRoot> teamList)
        {
            List<TeamRoot> championshipList = new List<TeamRoot>();

            
        
            foreach(var team in teamList)
            {
                if(team.Championships >= 1)
                {
                    championshipList.Add(team);
                }
            }

            championshipList.OrderBy(x => x.Championships).ToList();

            return championshipList; 
        }

        private static List<TeamRoot> NonChampionshipList(List<TeamRoot> teamList)
        {
            List<TeamRoot> championshipList = new List<TeamRoot>();


            foreach (var team in teamList)
            {
                if (team.Championships <= 0)
                {
                    championshipList.Add(team);
                }
            }

            championshipList.OrderBy(x => x.Championships).ToList();

            return championshipList;
        }

        private static List<PlayerRoot> WeightList(List<PlayerRoot> playerList)
        {
            List<PlayerRoot> newPlayerList = new List<PlayerRoot>();


            foreach (var player in playerList)
            {
                if (player.WeightPounds < 200)
                {
                    newPlayerList.Add(player);
                }
            }

            newPlayerList.OrderBy(x => x.WeightPounds).ToList();

            return newPlayerList;
        }


        private static List<PlayerRoot> HeightList(List<PlayerRoot> playerList)
        {
            List<PlayerRoot> newPlayerList = new List<PlayerRoot>();


            foreach (var player in playerList)
            {
                if (player.HeightFeet >= 7)
                {
                    newPlayerList.Add(player);
                }
            }

            newPlayerList.OrderBy(x => x.HeightFeet).ThenBy(x=> x.HeightInches).ToList();

            return newPlayerList;
        }




    }
}
