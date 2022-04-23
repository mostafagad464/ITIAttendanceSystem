#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITIAttendanceSystem.Data;
using ITIAttendanceSystem.Models;

namespace ITIAttendanceSystem.Views
{
    public class buildingAffairsAttendancesController : Controller
    {
        private readonly ITICOMPSYSDB2Context _context;

        public buildingAffairsAttendancesController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }

        // GET: buildingAffairsAttendances
        public async Task<IActionResult> Index()
        {
            var iTICOMPSYSDB2Context = _context.buildingAffairsAttendances.Include(b => b.Staff);
            return View(await iTICOMPSYSDB2Context.ToListAsync());
        }

        // GET: buildingAffairsAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingAffairsAttendance = await _context.buildingAffairsAttendances
                .Include(b => b.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (buildingAffairsAttendance == null)
            {
                return NotFound();
            }

            return View(buildingAffairsAttendance);
        }

        // GET: buildingAffairsAttendances/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.BuildingAffairsStaffs, "Id", "FullNameAr");
            return View();
        }

        // POST: buildingAffairsAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,AttendanceDate,ArrivalTime,LeaveTime")] buildingAffairsAttendance buildingAffairsAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildingAffairsAttendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.BuildingAffairsStaffs, "Id", "FullNameAr", buildingAffairsAttendance.StaffId);
            return View(buildingAffairsAttendance);
        }

        // GET: buildingAffairsAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingAffairsAttendance = await _context.buildingAffairsAttendances.FindAsync(id);
            if (buildingAffairsAttendance == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.BuildingAffairsStaffs, "Id", "FullNameAr", buildingAffairsAttendance.StaffId);
            return View(buildingAffairsAttendance);
        }

        // POST: buildingAffairsAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,AttendanceDate,ArrivalTime,LeaveTime")] buildingAffairsAttendance buildingAffairsAttendance)
        {
            if (id != buildingAffairsAttendance.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildingAffairsAttendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!buildingAffairsAttendanceExists(buildingAffairsAttendance.StaffId))
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
            ViewData["StaffId"] = new SelectList(_context.BuildingAffairsStaffs, "Id", "FullNameAr", buildingAffairsAttendance.StaffId);
            return View(buildingAffairsAttendance);
        }

        // GET: buildingAffairsAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingAffairsAttendance = await _context.buildingAffairsAttendances
                .Include(b => b.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (buildingAffairsAttendance == null)
            {
                return NotFound();
            }

            return View(buildingAffairsAttendance);
        }

        // POST: buildingAffairsAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingAffairsAttendance = await _context.buildingAffairsAttendances.FindAsync(id);
            _context.buildingAffairsAttendances.Remove(buildingAffairsAttendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool buildingAffairsAttendanceExists(int id)
        {
            return _context.buildingAffairsAttendances.Any(e => e.StaffId == id);
        }
    }
}
