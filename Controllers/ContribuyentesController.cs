using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Prueba_Tecnica.Models;
using MVC_Prueba_Tecnica.ViewModel;

namespace MVC_Prueba_Tecnica.Controllers
{
    public class ContribuyentesController : Controller
    {
        private readonly BddgiiContext _context;

        public ContribuyentesController(BddgiiContext context)
        {
            _context = context;
        }

        // GET: Contribuyentes
        [Route("Contribuyentes/lista")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TablaContribuyentes.ToListAsync());
        }

         public async Task<ActionResult> IndexbyId(string name)
         {


                 //var comprobantesContribuyente = _context.TablaContribuyentes.Join(_context.TablaComprobantesFiscales, cont => cont.RncCedula,
                 //comp => comp.RncCedula, (cont, comp) => new { comp, cont }).FirstOrDefault(x => x.cont.RncCedula == "98754321012");

                 var TodocomprobatesContribuyentes = await _context.TablaContribuyentes.GroupJoin(_context.TablaComprobantesFiscales, cont => cont.RncCedula,
                 comp => comp.RncCedula, (cont, comp) => new { cont, comp }).FirstOrDefaultAsync(x => x.cont.Nombre == name);

                 //var ContribuyentesConComprobantes = _context.TablaContribuyentes.GroupJoin(_context.TablaComprobantesFiscales, cont => cont.RncCedula,
                 //  comp => comp.RncCedula, (cont, comp) => new { cont, comp }).ToList();

                 return View(_context.TablaComprobantesFiscales.ToList());


         }

        // Acción para mostrar comprobantes fiscales y la suma total del ITBIS
       /* public async Task<ActionResult> Detalles(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                NotFound();
            }
            
            var contribuyente = await _context.TablaContribuyentes
                .Include(c => c.TablaComprobantesFiscales)
                .SingleOrDefaultAsync(c => c.RncCedula == id);

            if(contribuyente == null)
            {
                return NotFound();
            }

            var comprobantes = contribuyente.TablaComprobantesFiscales.ToList();
            ViewBag.TOTALITBIS = comprobantes.Sum(c => c.Itbis18);

            return View(comprobantes);
        }*/

        // GET: Contribuyentes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaContribuyente = await _context.TablaContribuyentes
                .FirstOrDefaultAsync(m => m.RncCedula == id);
            if (tablaContribuyente == null)
            {
                return NotFound();
            }

            return View(tablaContribuyente);
        }

        // GET: Contribuyentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contribuyentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContribuyente,RncCedula,Nombre,Tipo,Estatus")] TablaContribuyente tablaContribuyente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablaContribuyente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tablaContribuyente);
        }

        // GET: Contribuyentes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaContribuyente = await _context.TablaContribuyentes.FindAsync(id);
            if (tablaContribuyente == null)
            {
                return NotFound();
            }
            return View(tablaContribuyente);
        }

        // POST: Contribuyentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdContribuyente,RncCedula,Nombre,Tipo,Estatus")] TablaContribuyente tablaContribuyente)
        {
            if (id != tablaContribuyente.RncCedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablaContribuyente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablaContribuyenteExists(tablaContribuyente.RncCedula))
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
            return View(tablaContribuyente);
        }

        // GET: Contribuyentes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaContribuyente = await _context.TablaContribuyentes
                .FirstOrDefaultAsync(m => m.RncCedula == id);
            if (tablaContribuyente == null)
            {
                return NotFound();
            }

            return View(tablaContribuyente);
        }

        // POST: Contribuyentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tablaContribuyente = await _context.TablaContribuyentes.FindAsync(id);
            if (tablaContribuyente != null)
            {
                _context.TablaContribuyentes.Remove(tablaContribuyente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablaContribuyenteExists(string id)
        {
            return _context.TablaContribuyentes.Any(e => e.RncCedula == id);
        }
    }
}
