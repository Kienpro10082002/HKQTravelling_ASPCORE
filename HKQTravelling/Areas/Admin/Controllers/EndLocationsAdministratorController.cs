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
    [Route("EndLocationsAdministrator")]
    public class EndLocationsAdministratorController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EndLocationsAdministratorController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/EndLocationsAdministrator
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
              return _context.endLocations != null ? 
                          View(await _context.endLocations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.endLocations'  is null.");
        }

        // GET: Admin/EndLocationsAdministrator/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/EndLocationsAdministrator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(IFormCollection formColection)
        {
            string endLocation = formColection["EndLocationName"].ToString();
            if (endLocation == null)
            {
                ViewData["validation_message"] = "Điểm đến không được để trống!";
                return View();
            }
            else if (checkingEndLocation.checkEndLocationName(_context, endLocation))
            {
                ViewData["validation_message"] = "Điểm đến đã tồn tại!";
                return View();
            }
            else
            {
                var dbEndLocation = new EndLocations()
                {
                    EndLocationName = endLocation,
                };
                _context.Add(dbEndLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/EndLocationsAdministrator/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.endLocations == null)
            {
                return NotFound();
            }

            var endLocations = await _context.endLocations.FindAsync(id);
            if (endLocations == null)
            {
                return NotFound();
            }
            return View(endLocations);
        }

        // POST: Admin/EndLocationsAdministrator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, IFormCollection formCollection)
        {
            string endLocation = formCollection["EndLocationName"].ToString();
            var dbEndLocation = await _context.endLocations.FindAsync(id);
            try
            {
                if (endLocation == null)
                {
                    ViewData["validation_message"] = "Điểm đến không được để trống!";
                    return View();
                }
                else if (checkingEndLocation.checkEndLocationName(_context, endLocation))
                {
                    ViewData["validation_message"] = "Điểm đến đã tồn tại!";
                    return View();
                }
                else
                {
                    dbEndLocation.EndLocationName = endLocation;
                    _context.Update(dbEndLocation);
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

        // GET: Admin/EndLocationsAdministrator/Delete/5
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.endLocations == null)
            {
                return NotFound();
            }

            var endLocations = await _context.endLocations
                .FirstOrDefaultAsync(m => m.EndLocationId == id);
            if (endLocations == null)
            {
                return NotFound();
            }

            return View(endLocations);
        }

        // POST: Admin/EndLocationsAdministrator/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.endLocations == null)
            {
                return Problem("Entity set 'ApplicationDBContext.endLocations'  is null.");
            }
            var endLocations = await _context.endLocations.FindAsync(id);
            if (endLocations != null)
            {
                _context.endLocations.Remove(endLocations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EndLocationsExists(long id)
        {
          return (_context.endLocations?.Any(e => e.EndLocationId == id)).GetValueOrDefault();
        }
    }
}
