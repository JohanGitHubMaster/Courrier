using Courrier.Data;
using Courrier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courrier.Controllers
{
    public class LoginController : Controller
    {
        private readonly CourrierContext _context;

        public LoginController(CourrierContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> IndexCoursier()
        {          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexCoursier(Coursier coursier)
        {
            var cours = await _context.Coursier.FirstOrDefaultAsync(x=>x.Nom == coursier.Nom);
            if (cours != null)
            {
                return RedirectToAction("ListCourrierOfCoursier", "Courriers", new { coursierId = cours.Id });
            }
            return View();
        }
        public async Task<IActionResult> IndexReceptioniste()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexReceptioniste(Receptioniste receptioniste)
        {
            var reception = await _context.Receptioniste.FirstOrDefaultAsync(x => x.Nom == receptioniste.Nom);
            if (reception != null) 
            {
                return RedirectToAction("ListCourrierOfCoursier","Courriers", new { receptionisteId = reception.Id });
            }
            return View();
        }
        public async Task<IActionResult> IndexExpediteur()
        {
            return View();
        }
    }
}
