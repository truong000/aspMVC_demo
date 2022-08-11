using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCore_Demo.Models.EF;
using ASPCore_Demo.Models.WebMvcDbContext;

namespace ASPCore_Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly AspMvcDbContext _context;

        public ProductController(AspMvcDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index()
        {
            var listProduct = _context.ProductModels.ToList();
            return View(listProduct);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sreachString)
        {
            ViewData["GetProduct"] = sreachString;

            var product = from a in _context.ProductModels select a;
            if (!String.IsNullOrEmpty(sreachString))
            {
                product = product.Where(a => a.Name.Contains(sreachString));
            }
            return View(await product.AsNoTracking().ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductModels == null)
            {
                return NotFound();
            }

            var product = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,Price,Description,DateCreate")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.DateCreate = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductModels == null)
            {
                return NotFound();
            }

            var product = await _context.ProductModels.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,Price,Description,DateCreate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductModels == null)
            {
                return NotFound();
            }

            var product = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductModels == null)
            {
                return Problem("Entity set 'AspMvcDbContext.ProductModels'  is null.");
            }
            var product = await _context.ProductModels.FindAsync(id);
            if (product != null)
            {
                _context.ProductModels.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.ProductModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
