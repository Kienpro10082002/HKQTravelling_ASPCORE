using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HKQTravelling.Models;
using HKQTravelling.Areas.Admin.Extension;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("StartLocationsAdministrator")]
    public class StartLocationsAdministratorController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StartLocationsAdministratorController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/StartLocationsAdministrator
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
              return _context.startLocations != null ? 
                          View(await _context.startLocations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.startLocations'  is null.");
        }

        // GET: Admin/StartLocationsAdministrator/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/StartLocationsAdministrator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(IFormCollection formColection)
        {
            string startLocation = formColection["StartLocationName"].ToString();
            if (startLocation == null)
            {
                ViewData["validation_message"] = "Điểm khởi hành không được để trống!";
                return View();
            }
            else if (checkingStartLocation.checkStartLocationName(_context, startLocation))
            {
                ViewData["validation_message"] = "Điểm khởi hành đã tồn tại!";
                return View();
            }
            else
            {
                var dbStartLocation = new StartLocations()
                {
                    StartLocationName = startLocation,
                };
                _context.Add(dbStartLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/StartLocationsAdministrator/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.startLocations == null)
            {
                return NotFound();
            }

            var startLocations = await _context.startLocations.FindAsync(id);
            if (startLocations == null)
            {
                return NotFound();
            }
            return View(startLocations);
        }

        // POST: Admin/StartLocationsAdministrator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, IFormCollection formCollection)
        {
            string startLocation = formCollection["StartLocationName"].ToString();
            var dbStartLocation = await _context.startLocations.FindAsync(id);
            try
            {
                if (startLocation == null)
                {
                    ViewData["validation_message"] = "Điểm khởi hành không được để trống!";
                    return View();
                }
                else if (checkingStartLocation.checkStartLocationName(_context, startLocation))
                {
                    ViewData["validation_message"] = "Điểm khởi hành đã tồn tại!";
                    return View();
                }
                else
                {
                    dbStartLocation.StartLocationName = startLocation;
                    _context.Update(dbStartLocation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //Điều hướng đến trang lỗi
                return View();
            }
        }

        // GET: Admin/StartLocationsAdministrator/Delete/5
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.startLocations == null)
            {
                return NotFound();
            }

            var startLocations = await _context.startLocations
                .FirstOrDefaultAsync(m => m.StartLocationId == id);
            if (startLocations == null)
            {
                return NotFound();
            }

            return View(startLocations);
        }

        // POST: Admin/StartLocationsAdministrator/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.startLocations == null)
            {
                return Problem("Entity set 'ApplicationDBContext.startLocations'  is null.");
            }
            var startLocations = await _context.startLocations.FindAsync(id);
            if (startLocations != null)
            {
                _context.startLocations.Remove(startLocations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StartLocationsExists(long id)
        {
          return (_context.startLocations?.Any(e => e.StartLocationId == id)).GetValueOrDefault();
        }
    }
}
