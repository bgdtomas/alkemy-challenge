using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using alkemyChallengeCSharp.Database;
using alkemyChallengeCSharp.Models;
using usando_seguridad.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace alkemyChallengeCSharp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AlumnosController : Controller
    {
        private readonly AcademiaDbContext _context;

        public AlumnosController(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            try
            {
                alumno.Legajo.ValidarPassword();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Alumno.Password), ex.Message);
            }
            if (ModelState.IsValid)
            {
                alumno.Id = Guid.NewGuid();
                alumno.Legajo = alumno.Legajo;
                alumno.Password = alumno.Legajo.Encriptar();
                alumno.Username = alumno.Dni;
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Alumno alumno)
        {
            var alumnoLegajo = alumno.Legajo;
            if (!string.IsNullOrEmpty(alumnoLegajo))
            {
                try
                {
                    alumnoLegajo.ValidarPassword();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(Admin.Password), ex.Message);
                }
            }
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var alumnonDb = _context.Alumnos.Find(id);
                    alumnonDb.Nombre = alumno.Nombre;
                    alumnonDb.Apellido = alumno.Apellido;
                    alumnonDb.Dni = alumno.Dni;
                    alumnonDb.Username = alumno.Dni;
                    if (!string.IsNullOrEmpty(alumnoLegajo))
                    {
                        alumnonDb.Legajo = alumno.Legajo;
                        alumnonDb.Password = alumnoLegajo.Encriptar();
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
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
            return View(alumno);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(Guid id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
    }
}
