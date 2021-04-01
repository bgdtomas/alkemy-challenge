using System;
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
    public class AdminsController : Controller
    {
        private readonly AcademiaDbContext _context;

        public AdminsController(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Administradores.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin admin, string pass)
        {
            try
            {
                pass.ValidarPassword();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(nameof(Admin.Password), ex.Message);
            }
            if (ModelState.IsValid)
            {
                admin.Id = Guid.NewGuid();
                admin.Legajo = Guid.NewGuid();
                admin.Password = pass.Encriptar();
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administradores.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Admin admin, string pass)
        {
            if (!string.IsNullOrEmpty(pass))
            {
                try
                {
                    pass.ValidarPassword();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(nameof(Admin.Password), ex.Message);
                }
            }

            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var adminDb = _context.Administradores.Find(id);
                    adminDb.Nombre = admin.Nombre;
                    adminDb.Apellido = admin.Apellido;
                    adminDb.Username = admin.Username;
                    if (!string.IsNullOrEmpty(pass))
                    {
                        adminDb.Password = pass.Encriptar();
                    }




                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var admin = await _context.Administradores.FindAsync(id);
            _context.Administradores.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(Guid id)
        {
            return _context.Administradores.Any(e => e.Id == id);
        }
    }
}
