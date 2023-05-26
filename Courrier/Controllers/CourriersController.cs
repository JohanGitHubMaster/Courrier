using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courrier.Data;
using Courrier.Models;

namespace Courrier.Controllers
{
    public class CourriersController : Controller
    {
        private readonly CourrierContext _context;

        public CourriersController(CourrierContext context)
        {
            _context = context;
        }


        // GET: Courriers
        public async Task<IActionResult> Index()
        {
            var courrierDestinataire = new CourriersDestinataires();

            var courrierContext = _context.Courrier.Include(c => c.Coursier).Include(c => c.Flag).Include(c => c.Receptioniste).Include(c => c.Status)
                .Include(c=>c.CourrierDestinataires)!.ThenInclude(c=>c.Destinataire);

            return View(await courrierContext.ToListAsync());
        }

        // GET: Courriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courrier == null)
            {
                return NotFound();
            }

            var courrier = await _context.Courrier
                .Include(c => c.Coursier)
                .Include(c => c.Flag)
                .Include(c => c.Receptioniste)
                .Include(c => c.Status)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courrier == null)
            {
                return NotFound();
            }

            return View(courrier);
        }

        // GET: Courriers/Create
        public IActionResult Create()
        {
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Nom");
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Type");
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Nom");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Type");
            ViewData["DestinataireId"] = new SelectList(_context.Destinataire, "Id", "Nom");
            return View();
        }

        // POST: Courriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourriersDestinataires courrier)
        {
            if (ModelState.IsValid)
            {
                var Instancecourrier = courrier.courriers;
                Instancecourrier.CourrierDestinataires = new List<CourrierDestinataire>();
               
                foreach(var iddestinataire in courrier.destinataires)
                {
                    Instancecourrier.CourrierDestinataires.Add(new CourrierDestinataire { DestinataireId = iddestinataire });
                }
                _context.Add(Instancecourrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", courrier.courriers!.CoursierId);
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Id", courrier!.courriers.FlagId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", courrier!.courriers.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", courrier!.courriers.StatusId);
            ViewData["DestinataireId"] = new SelectList(_context.Destinataire, "Id", "Nom", courrier.destinataires);
            return View(courrier);
        }

        // GET: Courriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courrier == null)
            {
                return NotFound();
            }

            var courrier = await _context.Courrier.FindAsync(id);
            if (courrier == null)
            {
                return NotFound();
            }

            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Nom", courrier.CoursierId);
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Type", courrier.FlagId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Nom", courrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Type", courrier.StatusId);
            return View(courrier);
        }

        // POST: Courriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Réferences,Expediteur,Objet,Commentaire,CoursierId,ReceptionisteId,FlagId,StatusId")] Courriers courrier)
        {
            if (id != courrier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courrier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourrierExists(courrier.Id))
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
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", courrier.CoursierId);
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Id", courrier.FlagId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", courrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", courrier.StatusId);
            return View(courrier);
        }

        // GET: Courriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courrier == null)
            {
                return NotFound();
            }

            var courrier = await _context.Courrier
                .Include(c => c.Coursier)
                .Include(c => c.Flag)
                .Include(c => c.Receptioniste)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courrier == null)
            {
                return NotFound();
            }

            return View(courrier);
        }

        // POST: Courriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courrier == null)
            {
                return Problem("Entity set 'CourrierContext.Courrier'  is null.");
            }
            var courrier = await _context.Courrier.FindAsync(id);
            if (courrier != null)
            {
                _context.Courrier.Remove(courrier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourrierExists(int id)
        {
          return (_context.Courrier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
