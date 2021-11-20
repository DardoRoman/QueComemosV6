using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueComemosAppV6;
using QueComemosV6.Models;

namespace QueComemosV6.Controllers
{
    public class IngredienteController : Controller
    {
        private readonly QueComemosContext _context;

        public IngredienteController(QueComemosContext context)
        {
            _context = context;
        }

        // GET: Ingredientesontroller
        public async Task<IActionResult> Index()
        {
            var queComemosContext = _context.Ingredientes.Include(i => i.Receta);
            return View(await queComemosContext.ToListAsync());
        }

        // GET: Ingredientesontroller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.Receta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // GET: Ingredientesontroller/Create
        public IActionResult Create()
        {
            ViewData["RecetaId"] = new SelectList(_context.Receta, "Id", "Nombre");
            return View();
        }

        // POST: Ingredientesontroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Tipo,RecetaId")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecetaId"] = new SelectList(_context.Receta, "Id", "Nombre", ingrediente.RecetaId);
            return View(ingrediente);
        }

        // GET: Ingredientesontroller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            ViewData["RecetaId"] = new SelectList(_context.Receta, "Id", "Nombre", ingrediente.RecetaId);
            return View(ingrediente);
        }

        // POST: Ingredientesontroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Tipo,RecetaId")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id))
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
            ViewData["RecetaId"] = new SelectList(_context.Receta, "Id", "Nombre", ingrediente.RecetaId);
            return View(ingrediente);
        }

        // GET: Ingredientesontroller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.Receta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // POST: Ingredientesontroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }
    }
}
