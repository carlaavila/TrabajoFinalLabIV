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
    public class JugadoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ClubesController> _logger;

        public JugadoresController(ApplicationDbContext context, IWebHostEnvironment env, ILogger<ClubesController> logger)
		{
			_context = context;
            this.env = env;
            _logger = logger;
		}

        [AllowAnonymous]
        // GET: Jugadores
        public async Task<IActionResult> Index(int pagina = 1)
        {

            int RegistrosPorPagina = 2;

            var applicationDbContext = _context.Jugadores.Include(e => e.Club).Select(e => e);
           

            //generar pagina
            var registrosMostrar = applicationDbContext
                .Skip((pagina - 1) * RegistrosPorPagina)
                .Take(RegistrosPorPagina);

            //Crear el modelo para la vista
            JugadoresViewModel modelo = new JugadoresViewModel()
            {
                Jugadores = await registrosMostrar.ToListAsync(),
                Paginador = new PaginadorViewModel()
                {
                   PaginaActual = pagina,
                   RegistrosPorPagina = RegistrosPorPagina,
                   TotalRegistros = await applicationDbContext.CountAsync()
                 }

            };          

            return View(modelo);
        }

        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores
                 .Include(c => c.Club)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadores/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubes, "Id", "Nombre");
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Apellido,Nombres,Biografia,Foto,ClubId")] Jugador jugador)
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
                            var pathDestino = Path.Combine(env.WebRootPath, "images//jugadores");

                            // Generar nombre aleatorio de foto
                            var archivoDestino = Guid.NewGuid().ToString();
                            archivoDestino = archivoDestino.Replace("-", "");
                            archivoDestino += Path.GetExtension(archivoFoto.FileName);
                            var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                            using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                jugador.Foto = archivoDestino;
                            };

                            // Registrar que se ha guardado la foto con éxito
                            _logger.LogInformation("Se guardó la foto del jugador correctamente en: {Ruta}", rutaDestino);
                        }
                    }

                    _context.Add(jugador);
                    await _context.SaveChangesAsync();

                    // Registrar que se ha guardado el jugador con éxito
                    _logger.LogInformation("Se agregó un nuevo jugador con éxito.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // En caso de error, registrar la excepción
                _logger.LogError(ex, "Error al crear el jugador.");

                // Agregar un mensaje de error al ModelState
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el jugador.");
            }
            ViewData["ClubId"] = new SelectList(_context.Categorias, "Id", "Descripcion", jugador.ClubId);
            return View(jugador);
        }


        public async Task<IActionResult> Importar()
        {           

            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivo = archivos[0];

                if (archivo.Length > 0)
                {
                    var pathDestino = Path.Combine(env.WebRootPath, "importaciones");

                    // Generar nombre aleatorio de archivo
                    var archivoDestino = Guid.NewGuid().ToString();
                    archivoDestino = archivoDestino.Replace("-", "");
                    archivoDestino += Path.GetExtension(archivo.FileName);
                    var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                    using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        archivo.CopyTo(filestream);
                    };
                        var file = new FileStream(rutaDestino, FileMode.Open);

                        List<string> renglones = new List<string>();
                        List<Jugador> JugadorArch = new List<Jugador>();

                        StreamReader fileContent = new StreamReader(file, System.Text.Encoding.Default);
                        do
                        {
                            renglones.Add(fileContent.ReadLine());
                        }
                        while (!fileContent.EndOfStream);

                        foreach (var renglon in renglones)
                        {
                            Jugador jugador = new Jugador();
                            string[] data = renglon.Split(';');
                            if (data.Length == 3)
                            {
                                jugador.Apellido = data[0].Trim();
                                jugador.Nombres = data[1].Trim();
                                jugador.Biografia = data[2].Trim();
                                JugadorArch.Add(jugador);
                            }
                           
                        }
                    if (JugadorArch.Count() > 0)
                    {
                        _context.AddRange(JugadorArch);
                        await _context.SaveChangesAsync();
                    }

                }
            }

            return RedirectToAction("Index");
        }


        // GET: Jugadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            ViewBag.Clubes = new SelectList(_context.Clubes, "Id", "Nombre");

            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Apellido,Nombres,Biografia,Foto,ClubId")] Jugador jugador)
        {
            if (id != jugador.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(env.WebRootPath, "images//jugadores");
                    if (archivoFoto.Length > 0)
                    {

                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            string viejoArchivo = Path.Combine(pathDestino, jugador.Foto ?? "");
                            if (System.IO.File.Exists(viejoArchivo))
                                System.IO.File.Delete(viejoArchivo);

                            jugador.Foto = archivoDestino;
                        };

                    }
                }

                try
                {
                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.Id))
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
            ViewData["ClubId"] = new SelectList(_context.Categorias, "Id", "Nombre", jugador.ClubId);
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores
                 .Include(e => e.Club)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jugadores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jugadores'  is null.");
            }
            var jugador = await _context.Jugadores.FindAsync(id);
            if (jugador != null)
            {
                _context.Jugadores.Remove(jugador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorExists(int id)
        {
          return (_context.Jugadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
