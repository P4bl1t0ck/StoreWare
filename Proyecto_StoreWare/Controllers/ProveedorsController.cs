using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

namespace Proyecto_StoreWare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorsController : Controller
    {
        private readonly StoreWare _context;

        public ProveedorsController(StoreWare context)
        {
            _context = context;
        }
        //This controller doesn´t need those methods that are not necessary for the Proveedor entity.
        //As Transaccion, just needs Get ,search and delete methods.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
            return await _context.Proveedor.Include(p => p.Administrador).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedor.Include(p => p.Administrador).FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return proveedor;
        }
        /*
        [HttpDelete]
        public async Task<ActionResult<Proveedor>> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
        */
        //Bad idea if we delete a provider, we will lose all the products associated with it. 
        //I guess we can just disable the provider and not delete it.
        // GET: Proveedors
        public async Task<IActionResult> Index()
        {
            var storeWare = _context.Proveedor.Include(p => p.Administrador);
            return View(await storeWare.ToListAsync());
        }

        // GET: Proveedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(p => p.Administrador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedors/Create
        public IActionResult Create()
        {
            ViewData["AdministradorId"] = new SelectList(_context.Set<Administrador>(), "Id", "Contraseña");
            return View();
        }

        // POST: Proveedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdministradorId,Nombre,Email,Telefono")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministradorId"] = new SelectList(_context.Set<Administrador>(), "Id", "Contraseña", proveedor.AdministradorId);
            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewData["AdministradorId"] = new SelectList(_context.Set<Administrador>(), "Id", "Contraseña", proveedor.AdministradorId);
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdministradorId,Nombre,Email,Telefono")] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.Id))
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
            ViewData["AdministradorId"] = new SelectList(_context.Set<Administrador>(), "Id", "Contraseña", proveedor.AdministradorId);
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(p => p.Administrador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedor.Any(e => e.Id == id);
        }
    }
}
