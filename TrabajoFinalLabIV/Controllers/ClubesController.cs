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
    public class ClubesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ClubesController> _logger;

        public ClubesController(ApplicationDbContext context, IWebHostEnvironment env, ILogger<ClubesController> logger)
        {
            _context = context;
            this.env = env;
            _logger = logger;
        }

        [AllowAnonymous]
        // GET: Clubes
        public async Task<IActionResult> Index(string nombre, int? CategoriaId, string pais, int pagina = 1)
        {
            int RegistrosPorPagina = 2;


            var applicationDbContext = _context.Clubes.Include(e => e.Categoria).Select(e => e);
            if (!string.IsNullOrEmpty(nombre))
            {
                applicationDbContext = applicationDbContext.Where(e => e.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrEmpty(pais))
            {
                applicationDbContext = applicationDbContext.Where(e => e.Pais.Contains(pais));
            }
            if (CategoriaId.HasValue)
            {
                applicationDbContext = applicationDbContext.Where(e => e.CategoriaId == CategoriaId.Value);
            }

            //generar pagina
            var registrosMostrar = applicationDbContext
                .Skip((pagina - 1) * RegistrosPorPagina)
                .Take(RegistrosPorPagina);

            //Crear el modelo para la vista
            ClubesViewModel modelo = new ClubesViewModel()
            {
                Clubes = await registrosMostrar.ToListAsync(),
                ListCategorias = new SelectList(_context.Categorias, "Id", "Descripcion", CategoriaId),
                Nombre = nombre,
                Pais = pais,
                CategoriaId = CategoriaId,
                Paginador = new PaginadorViewModel()
                {
                    PaginaActual = pagina,
                    RegistrosPorPagina = RegistrosPorPagina,
                    TotalRegistros = await applicationDbContext.CountAsync()
                }
            };
          
            if (!string.IsNullOrEmpty(nombre))
            {
                modelo.Paginador.ValoresQueryString.Add("nombre", nombre);

            }
            if (!string.IsNullOrEmpty(pais))
            {
                modelo.Paginador.ValoresQueryString.Add("pais", pais);

            }
            if (CategoriaId.HasValue)
            {
                modelo.Paginador.ValoresQueryString.Add("CategoriaId", CategoriaId.Value.ToString());

            }

            return View(modelo);

        }

        // GET: Clubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var club = await _context.Clubes
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // GET: Clubes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View();
        }

        // POST: Clubes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Nombre,Resumen,FechaNacimiento,ImagenEscudo,Pais,CategoriaId")] Club club)
        {
            try
            {
                
                if (ModelState.IsValid)               
                {
                    var archivos = HttpContext.Request.Form.Files;
                        if (archivos != null && archivos.Count > 0)
                        {
                            var archivoFoto = archivos[0];
                    
                            if (archivoFoto.Length > 0)
                            {
                                var pathDestino = Path.Combine(env.WebRootPath, "images//clubes");

                                //generar nombre aleatorio de foto
                                var archivoDestino = Guid.NewGuid().ToString();
                                archivoDestino = archivoDestino.Replace("-", "");
                                archivoDestino += Path.GetExtension(archivoFoto.FileName);
                                var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                                using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                                {
                                    archivoFoto.CopyTo(filestream);
                                    club.ImagenEscudo = archivoDestino;
                                };

                            }
                        }

                    _context.Add(club);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un club.");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el club.");
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", club.CategoriaId);
            return View(club);
        }

        // GET: Clubes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {       

            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var club = await _context.Clubes.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", club.CategoriaId);
            return View(club);
        }

        // POST: Clubes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Resumen,FechaNacimiento,ImagenEscudo,Pais,CategoriaId")] Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(env.WebRootPath, "images//clubes");
                    if (archivoFoto.Length > 0)
                    {

                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            string viejoArchivo = Path.Combine(pathDestino, club.ImagenEscudo ?? "");
                            if (System.IO.File.Exists(viejoArchivo))
                                System.IO.File.Delete(viejoArchivo);

                            club.ImagenEscudo = archivoDestino;
                        };

                    }
                }

                try
                {
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", club.CategoriaId);
            return View(club);
        }

        // GET: Clubes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var club = await _context.Clubes
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // POST: Clubes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Clubes.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            var jugadoresAsociados = await _context.Jugadores
                .Where(j => j.ClubId == id)
                .ToListAsync();

            if (jugadoresAsociados.Any())
            {              
                TempData["ErrorMessage"] = "No se puede eliminar el club porque tiene jugadores asociados.";
                return View(club);
            }

            _context.Clubes.Remove(club);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ClubExists(int id)
        {
          return (_context.Clubes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
