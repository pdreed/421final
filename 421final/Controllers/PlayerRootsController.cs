#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _421final.Data;
using _421final.Models;
using System.Net;
using System.Text.Json;
using System.Globalization;

namespace _421final.Views
{
    public class PlayerRootsController : Controller
    {
        private ApplicationDbContext _context;

        public PlayerRootsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayerRoots
        public async Task<IActionResult> Index()
        {
            PlayerRootIndexVM vm = new PlayerRootIndexVM();
            vm.playerList = await _context.PlayerRoot.ToListAsync();
            vm.teamList = await _context.TeamRoot.ToListAsync();
            return View(vm);
        }

        // GET: PlayerRoots/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var playerRoot = await _context.PlayerRoot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerRoot == null)
            {
                return NotFound();
            }

            //getting the team from the TeamRoot context and passing it in the VM
            var dbTeamIdForPlayer = _context.PlayerRoot.Where(p => p.Id == id).Select(p => p.Team.Id);
            var dbTeam = await _context.TeamRoot.FirstOrDefaultAsync(m => m.Id == dbTeamIdForPlayer.First());

            PlayerRootTeamAndPlayerVM vm = new PlayerRootTeamAndPlayerVM();
            vm.player = playerRoot;
            vm.team = dbTeam;
            //NOT SURE IF THIS WORKS!!!!! -----------------------------------------------------
            while (true)
            {
                int year = 2021;
                string yearString = year.ToString();
                string playerID = id.ToString();
                string web1 = "https://www.balldontlie.io/api/v1/season_averages?season=";
                string web2 = "&player_ids[]=";
                string address = web1 + yearString + web2 + playerID;
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
                    AllSeasonAvgsRoot? playerStats = JsonSerializer.Deserialize<AllSeasonAvgsRoot>(jsonString);
                    if (playerStats.Data.Count != 0)
                    {
                        vm.stats = playerStats.Data[0];
                        break;
                    }
                    else
                    {
                        year--;
                        continue;
                    }
                }
            }
            //--------------------------------------------------------------------------------

            vm.stats.FgPct = vm.stats.FgPct * 100;
            vm.stats.Fg3Pct = vm.stats.Fg3Pct * 100;
            vm.stats.FtPct = vm.stats.FtPct * 100;

            vm.fgpct = (decimal?)vm.stats.FgPct;
            vm.fg3pct = (decimal?)vm.stats.Fg3Pct;
            vm.ftpct = (decimal?)vm.stats.FtPct;

            return View(vm);
        }

        // GET: PlayerRoots/Create
        public async Task<IActionResult> Create()
        {
            //Code to pull in all the players to the database
            /*for (int j = 1; j < 39; j++)
            {
                string num = j.ToString();
                string web = "https://www.balldontlie.io/api/v1/players?per_page=100&page=";
                string address = web + num;
                WebRequest request = WebRequest.Create(address);
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string jsonString = reader.ReadToEnd();
                        AllPlayersRoot? playerData = JsonSerializer.Deserialize<AllPlayersRoot>(jsonString);
                        //save to DB
                        foreach (var i in (IEnumerable<PlayerRoot>)playerData.Data)
                        {
                            
                            
                            var dbTeam = await _context.TeamRoot.FirstOrDefaultAsync(m => m.Id == i.Team.Id);
                            PlayerRoot newPlayer = new PlayerRoot();
                            newPlayer.Id = i.Id;
                            newPlayer.FirstName = i.FirstName;
                            newPlayer.HeightFeet = i.HeightFeet;
                            newPlayer.HeightInches = i.HeightInches;
                            newPlayer.LastName = i.LastName;
                            newPlayer.Position = i.Position;
                            newPlayer.Team = dbTeam;
                            newPlayer.WeightPounds = i.WeightPounds;
                            _context.Add(newPlayer);
                            
                            

                        }
                        _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] ON");
                        await _context.SaveChangesAsync();
                        _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] OFF");
                        transaction.Commit();
                    }

                }
            }*/
            return View();
        }

        // POST: PlayerRoots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,HeightFeet,HeightInches,LastName,Position,WeightPounds,Team")] PlayerRoot playerRoot)
        {
            var dbTeamForPlayer = _context.TeamRoot.Where(p => p.Abbreviation == playerRoot.Team.Abbreviation).Select(p => p);
            playerRoot.Team = dbTeamForPlayer.FirstOrDefault();
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Add(playerRoot);
                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] ON");
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] OFF");
                    transaction.Commit();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(playerRoot);
        }

        // GET: PlayerRoots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerRoot = await _context.PlayerRoot.FindAsync(id);
            if (playerRoot == null)
            {
                return NotFound();
            }
            return View(playerRoot);
        }

        // POST: PlayerRoots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,HeightFeet,HeightInches,LastName,Position,WeightPounds,Team")] PlayerRoot playerRoot)
        {
            if (id != playerRoot.Id)
            {
                return NotFound();
            }
            var dbTeamForPlayer = _context.TeamRoot.Where(p => p.Abbreviation == playerRoot.Team.Abbreviation).Select(p => p);
            playerRoot.Team = dbTeamForPlayer.FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.Update(playerRoot);
                        _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] ON");
                        await _context.SaveChangesAsync();
                        _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerRoot] OFF");
                        transaction.Commit();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerRootExists(playerRoot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(playerRoot);
        }

        // GET: PlayerRoots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerRoot = await _context.PlayerRoot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerRoot == null)
            {
                return NotFound();
            }

            var dbTeamIdForPlayer = _context.PlayerRoot.Where(p => p.Id == id).Select(p => p.Team.Id);
            var dbTeam = await _context.TeamRoot.FirstOrDefaultAsync(m => m.Id == dbTeamIdForPlayer.First());

            PlayerRootTeamAndPlayerVM vm = new PlayerRootTeamAndPlayerVM();
            vm.player = playerRoot;
            vm.team = dbTeam;

            return View(vm);
        }

        // POST: PlayerRoots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerRoot = await _context.PlayerRoot.FindAsync(id);
            _context.PlayerRoot.Remove(playerRoot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerRootExists(int id)
        {
            return _context.PlayerRoot.Any(e => e.Id == id);
        }
    }
}
