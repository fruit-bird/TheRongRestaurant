using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheRongRestaurant;
using TheRongRestaurant.Data;

namespace TheRongRestaurant.Controllers
{
    public class RestaurantTableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantTableController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantTable
        public async Task<IActionResult> Index()
        {
            return _context.Tables != null ?
                        View(await _context.Tables.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Tables'  is null.");
        }

        // GET: RestaurantTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var restaurantTable = await _context.Tables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantTable == null)
            {
                return NotFound();
            }

            return View(restaurantTable);
        }

        // GET: RestaurantTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RestaurantTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TableName,IsOccupied")] RestaurantTable restaurantTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurantTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurantTable);
        }

        // GET: RestaurantTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var restaurantTable = await _context.Tables.FindAsync(id);
            if (restaurantTable == null)
            {
                return NotFound();
            }
            return View(restaurantTable);
        }

        // POST: RestaurantTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TableName,IsOccupied")] RestaurantTable restaurantTable)
        {
            if (id != restaurantTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantTableExists(restaurantTable.Id))
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
            return View(restaurantTable);
        }

        // GET: RestaurantTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var restaurantTable = await _context.Tables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantTable == null)
            {
                return NotFound();
            }

            return View(restaurantTable);
        }

        // POST: RestaurantTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tables'  is null.");
            }
            var restaurantTable = await _context.Tables.FindAsync(id);
            if (restaurantTable != null)
            {
                _context.Tables.Remove(restaurantTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantTableExists(int id)
        {
            return (_context.Tables?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var allTables = _context.Tables.ToList();
                return View("Index", allTables);
            }

            var searchResults = _context.Tables
                .Where(table => EF.Functions.Like(table.TableName!, $"%{searchTerm}%"))
                .ToList();

            return View("Index", searchResults);
        }
    }
}
