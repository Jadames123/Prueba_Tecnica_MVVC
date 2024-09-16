using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Prueba_Tecnica.Models;

namespace MVC_Prueba_Tecnica.Controllers
{
    public class ComprobantesFiscalesController : Controller
    {
        private readonly BddgiiContext _context;

        public ComprobantesFiscalesController(BddgiiContext context)
        {
            _context = context;
        }

        // GET: ComprobantesFiscales
        [Route("Comprobantes/lista")]
        public async Task<IActionResult> Index()
        {
            var bddgiiContext = _context.TablaComprobantesFiscales.Include(t => t.RncCedulaNavigation);
            return View(await bddgiiContext.ToListAsync());
        }

        public ActionResult IndexbyId()
        {
            var comprobantesContribuyente = _context.TablaContribuyentes.Join(_context.TablaComprobantesFiscales, comp => comp.RncCedula,
                cont => cont.RncCedula, (dir, cont) => new {dir, cont}).FirstOrDefault(x => x.dir.RncCedula == "Juan perez");

            return View(_context.TablaComprobantesFiscales.ToList());

        }

        // GET: ComprobantesFiscales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaComprobantesFiscale = await _context.TablaComprobantesFiscales
                .Include(t => t.RncCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdComprobante == id);
            if (tablaComprobantesFiscale == null)
            {
                return NotFound();
            }

            return View(tablaComprobantesFiscale);
        }

        // GET: ComprobantesFiscales/Create
        public IActionResult Create()
        {
            ViewData["RncCedula"] = new SelectList(_context.TablaContribuyentes, "RncCedula", "RncCedula");
            return View();
        }

        // POST: ComprobantesFiscales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComprobante,RncCedula,Ncf,Monto,Itbis18")] TablaComprobantesFiscale tablaComprobantesFiscale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablaComprobantesFiscale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RncCedula"] = new SelectList(_context.TablaContribuyentes, "RncCedula", "RncCedula", tablaComprobantesFiscale.RncCedula);
            return View(tablaComprobantesFiscale);
        }

        // GET: ComprobantesFiscales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaComprobantesFiscale = await _context.TablaComprobantesFiscales.FindAsync(id);
            if (tablaComprobantesFiscale == null)
            {
                return NotFound();
            }
            ViewData["RncCedula"] = new SelectList(_context.TablaContribuyentes, "RncCedula", "RncCedula", tablaComprobantesFiscale.RncCedula);
            return View(tablaComprobantesFiscale);
        }

        // POST: ComprobantesFiscales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComprobante,RncCedula,Ncf,Monto,Itbis18")] TablaComprobantesFiscale tablaComprobantesFiscale)
        {
            if (id != tablaComprobantesFiscale.IdComprobante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablaComprobantesFiscale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablaComprobantesFiscaleExists(tablaComprobantesFiscale.IdComprobante))
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
            ViewData["RncCedula"] = new SelectList(_context.TablaContribuyentes, "RncCedula", "RncCedula", tablaComprobantesFiscale.RncCedula);
            return View(tablaComprobantesFiscale);
        }

        // GET: ComprobantesFiscales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablaComprobantesFiscale = await _context.TablaComprobantesFiscales
                .Include(t => t.RncCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdComprobante == id);
            if (tablaComprobantesFiscale == null)
            {
                return NotFound();
            }

            return View(tablaComprobantesFiscale);
        }

        // POST: ComprobantesFiscales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tablaComprobantesFiscale = await _context.TablaComprobantesFiscales.FindAsync(id);
            if (tablaComprobantesFiscale != null)
            {
                _context.TablaComprobantesFiscales.Remove(tablaComprobantesFiscale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablaComprobantesFiscaleExists(int id)
        {
            return _context.TablaComprobantesFiscales.Any(e => e.IdComprobante == id);
        }
    }
}
