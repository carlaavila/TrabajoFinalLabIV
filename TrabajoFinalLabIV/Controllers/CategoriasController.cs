using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalLabIV.Data;
using TrabajoFinalLabIV.Models;
using TrabajoFinalLabIV.ViewModel;

namespace TrabajoFinalLabIV.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClubesController> _logger;

        public CategoriasController(ApplicationDbContext context, ILogger<ClubesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]

        // GET: Categorias
        public async Task<IActionResult> Index(int pagina = 1)
        {
            int RegistrosPorPagina = 2;

            var applicationDbContext = _context.Jugadores;

            var registrosMostrar = _context.Categorias
               .Skip((pagina - 1) * RegistrosPorPagina)
               .Take(RegistrosPorPagina);

            //Crear el modelo para la vista
            CategoriasViewModel modelo = new CategoriasViewModel()
            {
                Categorias = await registrosMostrar.ToListAsync(),
                Paginador = new PaginadorViewModel()
                {
                    PaginaActual = pagina,
                    RegistrosPorPagina = RegistrosPorPagina,
                    TotalRegistros = await applicationDbContext.CountAsync()
                }

            };

            return View(modelo);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Categoria categoria)
        {
            try
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar la categoría en la base de datos.");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar la categoría en la base de datos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al crear la categoría.");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la categoría.");
            }


            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error de concurrencia al editar la categoría. CategoriaId: {CategoriaId}", categoria.Id);
                ModelState.AddModelError(string.Empty, "Otro usuario ha modificado esta categoría. Por favor, recargue la página y vuelva a intentarlo.");
                return View(categoria);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar la categoría editada en la base de datos. CategoriaId: {CategoriaId}, Descripcion: {Descripcion}", categoria.Id, categoria.Descripcion);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los cambios en la base de datos.");
                return View(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error desconocido al editar la categoría. CategoriaId: {CategoriaId}, Descripcion: {Descripcion}", categoria.Id, categoria.Descripcion);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar la categoría.");
                return View(categoria);
            }

            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            var clubesConCategoria = await _context.Clubes
                .Where(c => c.CategoriaId == id)
                .ToListAsync();

            if (clubesConCategoria.Any())
            {
                TempData["ErrorMessage"] = "No se puede eliminar la categoría porque tiene clubes asociados.";
                return View(categoria);
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        private bool CategoriaExists(int id)
        {
          return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
