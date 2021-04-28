using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using alkemyChallengeCSharp.Database;
using alkemyChallengeCSharp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace alkemyChallengeCSharp.Controllers
{
    [Authorize]
    public class MateriasController : Controller
    {
        private readonly AcademiaDbContext _context;

        public MateriasController(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["EsAlumno"] = false;
            if (ComprobarAlumno())
            {
                ViewData["EsAlumno"] = true;
            }

            var academiaDbContext = _context.Materias.Include(m => m.Profesor);
            return View(await academiaDbContext.OrderBy(x => x.Nombre).ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            ViewData["Test"] = false;
            if (ComprobarAlumno())
            {
                Guid? alumnoId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                RegistroAlumnosInscriptos? alumnoRegistrado = getTablaIntermedia(alumnoId, id);
                if (alumnoRegistrado != null)
                {
                    ViewData["Test"] = true;
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Materia materia)
        {
            if (ModelState.IsValid)
            {
                materia.Id = Guid.NewGuid();
                _context.Add(materia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materia.ProfesorId);
            return View(materia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materia.ProfesorId);
            return View(materia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Materia materia)
        {
            if (id != materia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.Id))
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
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materia.ProfesorId);
            return View(materia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var materia = await _context.Materias.FindAsync(id);
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(Guid id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult InscripcionMaterias(Materia MateriaId)
        {
            Materia Materia = _context.Materias.Find(MateriaId.Id);
            Guid alumnoLoginId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if ((alumnoLoginId != null && Materia.Id != null) && (Materia.MaxAlumnos >= 1))
            {
                var materiaRegistrada = new RegistroAlumnosInscriptos();
                materiaRegistrada.AlumnoId = alumnoLoginId;
                materiaRegistrada.MateriaId = Materia.Id;
                materiaRegistrada.Id = Guid.NewGuid();
                Materia.MaxAlumnos--;
                _context.RegistroAlumnosInscriptos.Add(materiaRegistrada);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DesuscribirMaterias(Materia Materia)
        {
            Guid alumnoLoginId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (alumnoLoginId != null && Materia.Id != null)
            {
                var tablaIntermedia = getTablaIntermedia(alumnoLoginId, Materia.Id);
                var materia = _context.Materias.Find(Materia.Id);
                materia.MaxAlumnos++;
                _context.RegistroAlumnosInscriptos.Remove(tablaIntermedia);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public RegistroAlumnosInscriptos getTablaIntermedia(Guid? alumnoId, Guid? id)
        {
            return _context.RegistroAlumnosInscriptos.Where(i => i.AlumnoId == alumnoId && i.MateriaId == id).FirstOrDefault();
        }

        public bool ComprobarAlumno()
        {
            bool esAlumno = false;
            Guid? userId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Alumno alumno = _context.Alumnos.Find(userId);
            if (alumno != null)
            {
                esAlumno = true;
            }
            return esAlumno;
        }
    }
}
