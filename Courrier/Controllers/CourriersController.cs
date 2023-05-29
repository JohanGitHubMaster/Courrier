using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courrier.Data;
using Courrier.Models;
using Microsoft.EntityFrameworkCore.Query;

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
        public async Task<IActionResult> IndexPaginate(string? searchstring, string? status,string? flags,  int take = 2, int skip = 0)
        {           
            ViewData["Status"] = new SelectList(_context.Status, "Id", "Type");
            ViewData["Flag"] = new SelectList(_context.Flag, "Id", "Type");


            var courrierDestinataire = new CourriersDestinataires();

            var courrierContext = _context.Courrier.Include(c => c.Coursier).Include(c => c.Flag).Include(c => c.Receptioniste).Include(c => c.Status)
                .Include(c => c.CourrierDestinataires)!.ThenInclude(c => c.Destinataire).Where(x=>x==x);

            if(!String.IsNullOrEmpty(searchstring))
            courrierContext = courrierContext.Where(x => x.Réferences!.Contains(searchstring) || x.Objet!.Contains(searchstring) || x.Expediteur!.Contains(searchstring));

            if(!String.IsNullOrEmpty(status))
                courrierContext = courrierContext.Where(x => x.StatusId==Int32.Parse(status));

            if(!String.IsNullOrEmpty(flags))
                courrierContext = courrierContext.Where(x => x.FlagId == Int32.Parse(flags));

            double TotalPage = await _context.Courrier.CountAsync() / (double)take;
            if (TotalPage % 2 == 0)
                ViewBag.TotalPage = (int)TotalPage;
            else
                ViewBag.TotalPage = (int)TotalPage + 1;

            return View(await courrierContext.Skip(skip).Take(take).ToListAsync());
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
                return RedirectToAction(nameof(IndexPaginate));
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
                return RedirectToAction(nameof(IndexPaginate));
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
            return RedirectToAction(nameof(IndexPaginate));
        }

        public async Task<IActionResult> ListCourrierOfCoursier(int?coursierId,int? receptionisteId)
        {

            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Nom");
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Type");
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Nom");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Type");
            ViewData["DestinataireId"] = new SelectList(_context.Destinataire, "Id", "Nom");
            ViewData["isReceptioniste"]=null;
            var courrierDestinataire = new CourriersDestinataires();
            IIncludableQueryable<Courriers, Destinataire?> courrierContext;
            if (receptionisteId != null)
            {
                TempData["isReceptioniste"] = true;
                courrierContext = _context.Courrier.Where(x => x.ReceptionisteId == receptionisteId).Include(c => c.Coursier).Include(c => c.Flag).Include(c => c.Receptioniste).Include(c => c.Status).Include(c => c.CourrierDestinataires)!.ThenInclude(c => c.Destinataire);
            }
            else
            {
                TempData["isReceptioniste"] = false;
                courrierContext = _context.Courrier.Where(x => x.CoursierId == coursierId).Include(c => c.Coursier).Include(c => c.Flag).Include(c => c.Receptioniste).Include(c => c.Status).Include(c => c.CourrierDestinataires)!.ThenInclude(c => c.Destinataire);
            }
            return View(await courrierContext.ToListAsync());
        }



        public async Task<IActionResult> EditStatus(int? id)
        {
         
            var courrierDestinataire = new CourriersDestinataires();
            

            var courrierContext = await _context.Courrier.FindAsync(id);
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Nom", courrierContext.Id);
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Type", courrierContext.FlagId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Nom", courrierContext.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Type", courrierContext.StatusId);

            if (!(Boolean)TempData["isReceptioniste"] && courrierContext.StatusId == 2)
            {
                TempData["isReceptioniste"] = false;
                return View(courrierContext);
            }
            else if((Boolean)TempData["isReceptioniste"] && courrierContext.StatusId == 1)
            {
                TempData["isReceptioniste"] = true;
                return View(courrierContext);

            }
            if (!(Boolean)TempData["isReceptioniste"])
                return RedirectToAction(nameof(ListCourrierOfCoursier), new { coursierId = courrierContext.CoursierId });
            else
                return RedirectToAction(nameof(ListCourrierOfCoursier), new { receptionisteId = courrierContext.ReceptionisteId });

            //return View(courrierContext);
        }

        public async Task<IActionResult> EditMouvement(Courriers courrier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _context.Update(courrier);
                   
                    courrier.MouvementCourriers = new List<MouvementCourrier>();
                    if (!(Boolean)TempData["isReceptioniste"])
                    {
                        courrier.StatusId = 1;
                        
                        courrier.MouvementCourriers.Add(new MouvementCourrier { CoursierId = courrier.CoursierId, DatedeMouvement = DateTime.Now, StatusId = courrier.StatusId });
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(ListCourrierOfCoursier), new { coursierId = courrier.CoursierId });
                    }
                    else if((Boolean)TempData["isReceptioniste"])
                    {
                        courrier.StatusId = 3;
                        courrier.MouvementCourriers.Add(new MouvementCourrier { ReceptionisteId = courrier.ReceptionisteId, DatedeMouvement = DateTime.Now, StatusId = courrier.StatusId });
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(ListCourrierOfCoursier), new { receptionisteId = courrier.ReceptionisteId });
                    }
                    
                    return RedirectToAction(nameof(IndexPaginate));
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
                
            }
            ViewData["CoursierId"] = new SelectList(_context.Coursier, "Id", "Id", courrier.CoursierId);
            ViewData["FlagId"] = new SelectList(_context.Set<Flag>(), "Id", "Id", courrier.FlagId);
            ViewData["ReceptionisteId"] = new SelectList(_context.Receptioniste, "Id", "Id", courrier.ReceptionisteId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", courrier.StatusId);
            return View(EditStatus);
        }



        private bool CourrierExists(int id)
        {
          return (_context.Courrier?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        
    }
}
