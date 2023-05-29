using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courrier.Data;
using Courrier.Models;
using Rotativa.AspNetCore;

namespace Courrier.Controllers
{
    public class MouvementCourriersController : Controller
    {
        private readonly CourrierContext _context;

        public MouvementCourriersController(CourrierContext context)
        {
            _context = context;
        }

        // GET: MouvementCourriers
        public async Task<IActionResult> Index(int? id)
        {
            var courrierContext = _context.MouvementCourrier.Where(x=>x.Courriers.Id == id).Include(m => m.Courriers).Include(m => m.Coursier).Include(m => m.Receptioniste).Include(m => m.Status);
            if (courrierContext.Count()<=0) return RedirectToAction("IndexPaginate", "Courriers");
            return View(await courrierContext.ToListAsync());
        }

        // GET: MouvementCourriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MouvementCourrier == null)
            {
                return NotFound();
            }

            var mouvementCourrier = await _context.MouvementCourrier
                .Include(m => m.Courriers)
                .Include(m => m.Coursier)
                .Include(m => m.Receptioniste)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouvementCourrier == null)
            {
                return NotFound();
            }

            return View(mouvementCourrier);
        }

        // GET: MouvementCourriers/Create
        public IActionResult Create()
        {
            ViewData["CourriersId"] = new SelectList(_context.Courrier, "Id", "Id");
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id");
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
            return View();
        }

        // POST: MouvementCourriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CoursierId,CourriersId,StatusId,ReceptionisteId,DatedeMouvement")] MouvementCourrier mouvementCourrier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mouvementCourrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourriersId"] = new SelectList(_context.Courrier, "Id", "Id", mouvementCourrier.CourriersId);
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", mouvementCourrier.CoursierId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", mouvementCourrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", mouvementCourrier.StatusId);
            return View(mouvementCourrier);
        }

        // GET: MouvementCourriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MouvementCourrier == null)
            {
                return NotFound();
            }

            var mouvementCourrier = await _context.MouvementCourrier.FindAsync(id);
            if (mouvementCourrier == null)
            {
                return NotFound();
            }
            ViewData["CourriersId"] = new SelectList(_context.Courrier, "Id", "Id", mouvementCourrier.CourriersId);
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", mouvementCourrier.CoursierId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", mouvementCourrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", mouvementCourrier.StatusId);
            return View(mouvementCourrier);
        }

        // POST: MouvementCourriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CoursierId,CourriersId,StatusId,ReceptionisteId,DatedeMouvement")] MouvementCourrier mouvementCourrier)
        {
            if (id != mouvementCourrier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mouvementCourrier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MouvementCourrierExists(mouvementCourrier.Id))
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
            ViewData["CourriersId"] = new SelectList(_context.Courrier, "Id", "Id", mouvementCourrier.CourriersId);
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", mouvementCourrier.CoursierId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", mouvementCourrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", mouvementCourrier.StatusId);
            return View(mouvementCourrier);
        }

        // GET: MouvementCourriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MouvementCourrier == null)
            {
                return NotFound();
            }

            var mouvementCourrier = await _context.MouvementCourrier
                .Include(m => m.Courriers)
                .Include(m => m.Coursier)
                .Include(m => m.Receptioniste)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouvementCourrier == null)
            {
                return NotFound();
            }

            return View(mouvementCourrier);
        }

        // POST: MouvementCourriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MouvementCourrier == null)
            {
                return Problem("Entity set 'CourrierContext.MouvementCourrier'  is null.");
            }
            var mouvementCourrier = await _context.MouvementCourrier.FindAsync(id);
            if (mouvementCourrier != null)
            {
                _context.MouvementCourrier.Remove(mouvementCourrier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DemoViewAsPDF()
        {
           return new Rotativa.AspNetCore.ViewAsPdf("DemoViewAsPDF");
        }

        private bool MouvementCourrierExists(int id)
        {
          return (_context.MouvementCourrier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
