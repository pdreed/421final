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
    public class TeamStylesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamStylesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamStyles
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamStyle.ToListAsync());
        }

        // GET: TeamStyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStyle = await _context.TeamStyle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamStyle == null)
            {
                return NotFound();
            }

            return View(teamStyle);
        }

        // GET: TeamStyles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,style")] TeamStyle teamStyle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamStyle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamStyle);
        }

        // GET: TeamStyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStyle = await _context.TeamStyle.FindAsync(id);
            if (teamStyle == null)
            {
                return NotFound();
            }
            return View(teamStyle);
        }

        // POST: TeamStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,style")] TeamStyle teamStyle)
        {
            if (id != teamStyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamStyle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamStyleExists(teamStyle.Id))
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
            return View(teamStyle);
        }

        // GET: TeamStyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStyle = await _context.TeamStyle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamStyle == null)
            {
                return NotFound();
            }

            return View(teamStyle);
        }

        // POST: TeamStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamStyle = await _context.TeamStyle.FindAsync(id);
            _context.TeamStyle.Remove(teamStyle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamStyleExists(int id)
        {
            return _context.TeamStyle.Any(e => e.Id == id);
        }
    }
}
