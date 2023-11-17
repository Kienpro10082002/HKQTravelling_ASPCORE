using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HKQTravelling.Models;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ToursAdministratorController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ToursAdministratorController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/ToursAdministrator
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.tours.Include(t => t.discounts).Include(t => t.endLocations).Include(t => t.startLocations);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Admin/ToursAdministrator/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tours = await _context.tours
                .Include(t => t.discounts)
                .Include(t => t.endLocations)
                .Include(t => t.startLocations)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tours == null)
            {
                return NotFound();
            }

            return View(tours);
        }

        // GET: Admin/ToursAdministrator/Create
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.discounts, "DiscountId", "DiscountId");
            ViewData["EndLocationId"] = new SelectList(_context.endLocations, "EndLocationId", "EndLocationName");
            ViewData["StartLocationId"] = new SelectList(_context.startLocations, "StartLocationId", "StartLocationName");
            return View();
        }

        // POST: Admin/ToursAdministrator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,TourName,Price,StartDate,EndDate,Status,CreationDate,UpdateDate,Remaining,DiscountId,StartLocationId,EndLocationId")] Tours tours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(_context.discounts, "DiscountId", "DiscountId", tours.DiscountId);
            ViewData["EndLocationId"] = new SelectList(_context.endLocations, "EndLocationId", "EndLocationName", tours.EndLocationId);
            ViewData["StartLocationId"] = new SelectList(_context.startLocations, "StartLocationId", "StartLocationName", tours.StartLocationId);
            return View(tours);
        }

        // GET: Admin/ToursAdministrator/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tours = await _context.tours.FindAsync(id);
            if (tours == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.discounts, "DiscountId", "DiscountId", tours.DiscountId);
            ViewData["EndLocationId"] = new SelectList(_context.endLocations, "EndLocationId", "EndLocationName", tours.EndLocationId);
            ViewData["StartLocationId"] = new SelectList(_context.startLocations, "StartLocationId", "StartLocationName", tours.StartLocationId);
            return View(tours);
        }

        // POST: Admin/ToursAdministrator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TourId,TourName,Price,StartDate,EndDate,Status,CreationDate,UpdateDate,Remaining,DiscountId,StartLocationId,EndLocationId")] Tours tours)
        {
            if (id != tours.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToursExists(tours.TourId))
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
            ViewData["DiscountId"] = new SelectList(_context.discounts, "DiscountId", "DiscountId", tours.DiscountId);
            ViewData["EndLocationId"] = new SelectList(_context.endLocations, "EndLocationId", "EndLocationName", tours.EndLocationId);
            ViewData["StartLocationId"] = new SelectList(_context.startLocations, "StartLocationId", "StartLocationName", tours.StartLocationId);
            return View(tours);
        }

        // GET: Admin/ToursAdministrator/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tours = await _context.tours
                .Include(t => t.discounts)
                .Include(t => t.endLocations)
                .Include(t => t.startLocations)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tours == null)
            {
                return NotFound();
            }

            return View(tours);
        }

        // POST: Admin/ToursAdministrator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.tours == null)
            {
                return Problem("Entity set 'ApplicationDBContext.tours'  is null.");
            }
            var tours = await _context.tours.FindAsync(id);
            if (tours != null)
            {
                _context.tours.Remove(tours);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToursExists(long id)
        {
          return (_context.tours?.Any(e => e.TourId == id)).GetValueOrDefault();
        }
    }
}
