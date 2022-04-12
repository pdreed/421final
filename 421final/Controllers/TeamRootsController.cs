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

namespace _421final.Views
{
    public class TeamRootsController : Controller
    {
        private ApplicationDbContext _context;

        public TeamRootsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamRoots
        public async Task<IActionResult> Index()
        {
                return View(await _context.TeamRoot.ToListAsync());
        }

        /*[HttpPost]
        public ActionResult RecordCard(TeamRoot Model)
        {

            _ = Create(Model);
            return View(Model);
        }*/


        // GET: TeamRoots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamRoot = await _context.TeamRoot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamRoot == null)
            {
                return NotFound();
            }

            /*List<TeamRoot> listOfTeams = new List<TeamRoot>();

            WebRequest request = WebRequest.Create("https://www.balldontlie.io/api/v1/teams");
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
                AllTeamsRoot? teamData = JsonSerializer.Deserialize<AllTeamsRoot>(jsonString);
                //save to DB
                foreach (var i in (IEnumerable<AllTeamsDef>)teamData.Data)
                {
                    TeamRoot newTeam = new TeamRoot();
                    //newTeam.Id = i.Id;
                    newTeam.Abbreviation = i.Abbreviation;
                    newTeam.City = i.City;
                    newTeam.Conference = i.Conference;
                    newTeam.Division = i.Division;
                    newTeam.FullName = i.FullName;
                    newTeam.Name = i.Name;
                    listOfTeams.Add(newTeam);

                }

            }
            for (int i = 0; i < 30; i++)
            {
                _ = Create(listOfTeams[i]);
            }*/

            return View(teamRoot);
        }

        // GET: TeamRoots/Create
        public async Task<IActionResult> Create()
        {

            WebRequest request = WebRequest.Create("https://www.balldontlie.io/api/v1/teams");
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
                    AllTeamsRoot? teamData = JsonSerializer.Deserialize<AllTeamsRoot>(jsonString);
                    //save to DB
                    //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamRoot] ON");
                    //await _context.SaveChangesAsync();
                    foreach (var i in (IEnumerable<TeamRoot>)teamData.Data)
                    {
                        TeamRoot newTeam = new TeamRoot();
                        newTeam.Id = i.Id;
                        newTeam.Abbreviation = i.Abbreviation;
                        newTeam.City = i.City;
                        newTeam.Conference = i.Conference;
                        newTeam.Division = i.Division;
                        newTeam.FullName = i.FullName;
                        newTeam.Name = i.Name;
                        _context.Add(newTeam);
                    }
                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamRoot] ON");
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamRoot] OFF");
                    transaction.Commit();
                }

            }
            
            return View();
        }

        // POST: TeamRoots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Abbreviation,City,Conference,Division,FullName,Name,Championships")] TeamRoot teamRoot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamRoot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamRoot);
        }

        // GET: TeamRoots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamRoot = await _context.TeamRoot.FindAsync(id);
            if (teamRoot == null)
            {
                return NotFound();
            }
            return View(teamRoot);
        }

        // POST: TeamRoots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Abbreviation,City,Conference,Division,FullName,Name,Championships")] TeamRoot teamRoot)
        {
            if (id != teamRoot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamRoot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamRootExists(teamRoot.Id))
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
            return View(teamRoot);
        }

        // GET: TeamRoots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamRoot = await _context.TeamRoot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamRoot == null)
            {
                return NotFound();
            }

            return View(teamRoot);
        }

        // POST: TeamRoots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamRoot = await _context.TeamRoot.FindAsync(id);
            _context.TeamRoot.Remove(teamRoot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamRootExists(int id)
        {
            return _context.TeamRoot.Any(e => e.Id == id);
        }
    }
}
