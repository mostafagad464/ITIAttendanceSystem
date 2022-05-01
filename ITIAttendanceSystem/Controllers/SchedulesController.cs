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
    public class SchedulesController : Controller
    {
        private readonly ITICOMPSYSDB2Context _context;

        public SchedulesController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName");

            //var iTICOMPSYSDB2Context = _context.Schedules.Include(s => s.Department);
            //return View(await iTICOMPSYSDB2Context.ToListAsync());
            return View();
        }


        public async Task<IActionResult> GetDeptSchedule(int DepartmentId, DateTime DateFrom, DateTime DateTo)
        {
            List<Schedule> model = new List<Schedule>();
            //Department department = _context.Departments.FirstOrDefault(a => a.Id == DepartmentId);

            model = _context.Schedules.Where(a => a.ScheduleDate >= DateFrom && a.ScheduleDate <= DateTo.AddDays(1)  && a.DepartmentId == DepartmentId).ToList();
            
            int totalDays = (int)(DateTo - DateFrom).TotalDays ;
            DayOfWeek dayOfWeek = DateFrom.DayOfWeek;
            int numDayOfWeek = (int) DateFrom.DayOfWeek + 1;
            int daySchedule = totalDays + numDayOfWeek;
            int constDaySchedule = daySchedule / 7;
            if (daySchedule % 7 != 0)
                constDaySchedule += 1;

            ViewData["DateFrom"] = DateFrom;
            ViewData["DateTo"] = DateTo;
            ViewData["totalDays"] = totalDays;
            ViewData["dayOfWeek"] = dayOfWeek;
            ViewData["numDayOfWeek"] = numDayOfWeek;
            ViewData["daySchedule"] = daySchedule;
            ViewData["constDaySchedule"] = constDaySchedule;

            return PartialView(model);
        }

        public async Task<IActionResult> UpdateSchedule(int DepartmentId, DateTime DateFrom, DateTime DateTo, DateTime[] DateArr, int[] PeriodArr)
        {
            for (int i = 0; i < PeriodArr.Length; i++)
            {
                Schedule schedule = _context.Schedules.FirstOrDefault(a => a.DepartmentId == DepartmentId && a.ScheduleDate == DateArr[i]);
                if(schedule != null)
                {
                    schedule.LectPeriod = PeriodArr[i];
                    _context.Update(schedule);
                }
                else
                {
                    schedule = new Schedule();
                    schedule.DepartmentId= DepartmentId;
                    schedule.ScheduleDate = DateArr[i];
                    schedule.LectPeriod = PeriodArr[i];
                    _context.Add(schedule);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("GetDeptSchedule",new { DepartmentId = DepartmentId, DateFrom = DateFrom, DateTo = DateTo});
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,ScheduleDate,LectPeriod")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", schedule.DepartmentId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", schedule.DepartmentId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,ScheduleDate,LectPeriod")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", schedule.DepartmentId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }




    }
}
