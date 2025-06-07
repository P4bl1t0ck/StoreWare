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
    public class TransaccionsController : Controller
    {
        private readonly StoreWare _context;

        public TransaccionsController(StoreWare context)
        {
            _context = context;
        }

        // GET: Transaccions
        public async Task<IActionResult> Index()
        {
            var storeWare = _context.Transaccion.Include(t => t.Pago).Include(t => t.Producto).Include(t => t.Usuario);
            return View(await storeWare.ToListAsync());
        }
        //Here we should create a method to search for transactions by user, product, or date.
        //We just need to show our transactions and delete them if necessary.
        //So we will not create a search method for now, but we can add it later if needed.
        //Method to show all transactions.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccions() 
        {
            return await _context.Transaccion.ToListAsync();
        }
        //Method to get a specific transaction by id.
        [HttpGet]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id) 
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return transaccion;
        }
        //Method for delete transaction by id.
        [HttpDelete]
        public async Task<ActionResult<Transaccion>> DeleteTransaccion(int id) 
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }else
            {
                _context.Transaccion.Remove(transaccion);
                await _context.SaveChangesAsync();
                return transaccion;
            }

            //return null;
        }
        // GET: Transaccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transaccions/Create
        public IActionResult Create()
        {
            ViewData["PagoId"] = new SelectList(_context.Set<Pago>(), "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Categoria");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña");
            return View();
        }

        // POST: Transaccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,ProductoId,PagoId,Cantidad,Fecha,Estado")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PagoId"] = new SelectList(_context.Set<Pago>(), "Id", "Nombre", transaccion.PagoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Categoria", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", transaccion.UsuarioId);
            return View(transaccion);
        }

        // GET: Transaccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            ViewData["PagoId"] = new SelectList(_context.Set<Pago>(), "Id", "Nombre", transaccion.PagoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Categoria", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", transaccion.UsuarioId);
            return View(transaccion);
        }

        // POST: Transaccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,ProductoId,PagoId,Cantidad,Fecha,Estado")] Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.Id))
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
            ViewData["PagoId"] = new SelectList(_context.Set<Pago>(), "Id", "Nombre", transaccion.PagoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Categoria", transaccion.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", transaccion.UsuarioId);
            return View(transaccion);
        }

        // GET: Transaccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transaccion
                .Include(t => t.Pago)
                .Include(t => t.Producto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transaccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccion = await _context.Transaccion.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transaccion.Remove(transaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transaccion.Any(e => e.Id == id);
        }
    }
}
