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

namespace _421final.Views
{
    public class MyTeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyTeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyTeams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MyTeam.Include(m => m.TeamStyle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MyTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTeam = await _context.MyTeam
                .Include(m => m.TeamStyle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTeam == null)
            {
                return NotFound();
            }

            return View(myTeam);
        }

        // GET: MyTeams/Create
        public IActionResult Create()
        {
            ViewData["TeamStyleId"] = new SelectList(_context.TeamStyle, "Id", "Id");
            return View();
        }

        // POST: MyTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamStyleId,teamName,PG,SG,SF,PF,C")] MyTeam myTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamStyleId"] = new SelectList(_context.TeamStyle, "Id", "Id", myTeam.TeamStyleId);
            return View(myTeam);
        }

        // GET: MyTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTeam = await _context.MyTeam.FindAsync(id);
            if (myTeam == null)
            {
                return NotFound();
            }
            ViewData["TeamStyleId"] = new SelectList(_context.TeamStyle, "Id", "Id", myTeam.TeamStyleId);
            return View(myTeam);
        }

        // POST: MyTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamStyleId,teamName,PG,SG,SF,PF,C")] MyTeam myTeam)
        {
            if (id != myTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyTeamExists(myTeam.Id))
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
            ViewData["TeamStyleId"] = new SelectList(_context.TeamStyle, "Id", "Id", myTeam.TeamStyleId);
            return View(myTeam);
        }

        // GET: MyTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTeam = await _context.MyTeam
                .Include(m => m.TeamStyle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTeam == null)
            {
                return NotFound();
            }

            return View(myTeam);
        }

        // POST: MyTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myTeam = await _context.MyTeam.FindAsync(id);
            _context.MyTeam.Remove(myTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyTeamExists(int id)
        {
            return _context.MyTeam.Any(e => e.Id == id);
        }
    }
}
