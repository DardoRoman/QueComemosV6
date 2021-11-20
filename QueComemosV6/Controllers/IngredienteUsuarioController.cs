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
    public class IngredienteUsuarioController : Controller
    {
        private readonly QueComemosContext _context;

        public IngredienteUsuarioController(QueComemosContext context)
        {
            _context = context;
        }

        // GET: IngredienteUsuario
        public async Task<IActionResult> Index()
        {
            var queComemosContext = _context.MisIngredientes.Include(i => i.Usuario);
            return View(await queComemosContext.ToListAsync());
        }

        // GET: IngredienteUsuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteUsuario = await _context.MisIngredientes
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredienteUsuario == null)
            {
                return NotFound();
            }

            return View(ingredienteUsuario);
        }

        // GET: IngredienteUsuario/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: IngredienteUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,Tipo,UsuarioId")] IngredienteUsuario ingredienteUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredienteUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", ingredienteUsuario.UsuarioId);
            return View(ingredienteUsuario);
        }

        // GET: IngredienteUsuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteUsuario = await _context.MisIngredientes.FindAsync(id);
            if (ingredienteUsuario == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", ingredienteUsuario.UsuarioId);
            return View(ingredienteUsuario);
        }

        // POST: IngredienteUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Tipo,UsuarioId")] IngredienteUsuario ingredienteUsuario)
        {
            if (id != ingredienteUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredienteUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteUsuarioExists(ingredienteUsuario.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", ingredienteUsuario.UsuarioId);
            return View(ingredienteUsuario);
        }

        // GET: IngredienteUsuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteUsuario = await _context.MisIngredientes
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredienteUsuario == null)
            {
                return NotFound();
            }

            return View(ingredienteUsuario);
        }

        // POST: IngredienteUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredienteUsuario = await _context.MisIngredientes.FindAsync(id);
            _context.MisIngredientes.Remove(ingredienteUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteUsuarioExists(int id)
        {
            return _context.MisIngredientes.Any(e => e.Id == id);
        }
    }
}
