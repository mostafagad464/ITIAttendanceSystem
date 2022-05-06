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
    public class AttendancesController : Controller
    {
        private readonly ITICOMPSYSDB2Context _context;

        public AttendancesController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }

        // GET: Attendances
        public IActionResult Index()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }
        // GET: Attendances/IndexOnline
        public IActionResult IndexOnline()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }
        public IActionResult DepartmentStudents(int DepartmentID, string Status, DateTime? AttDate)
        {
            List<Student> studentsList = new List<Student>();

            Department dept = _context.Departments.Include(s => s.Students).FirstOrDefault(x => x.Id == DepartmentID);
            List<Student> StudentsInDept = dept.Students.ToList();

            DateTime today = (AttDate == null) ? DateTime.Today : (DateTime)AttDate;
            List<Attendance> Attended = _context.Attendances.Include(s => s.Student).Where(a => a.AttendanceDate == today && a.Student.DepartmentId == DepartmentID &&a.LeaveTime == null).ToList();
            List<Student> StudentsAttended = new List<Student>();

            foreach (Attendance attendance in Attended)
            {
                StudentsAttended.Add(attendance.Student);
            }

            List<Student> StudentsNotAttend = StudentsInDept.Except(StudentsAttended).ToList();

            ViewBag.Status = Status;
            if (Status == "Arriving")
            {
                studentsList = StudentsNotAttend;
            }
            else if (Status == "Leaving")
            {
                studentsList = StudentsAttended;
            }
            return PartialView(studentsList);
        }
        public async Task<IActionResult> Attend(int id, string stat, int deptId, DateTime? AttDate, TimeSpan? AttTime)
        {
            Attendance StdAttend;

            AttDate = (AttDate == null) ? DateTime.Today : AttDate;
            AttTime = (AttTime == null) ? DateTime.Now.TimeOfDay : AttTime;

            if (stat == "Arriving")
            {
                StdAttend = new Attendance();
                StdAttend.StudentId = id;
                StdAttend.AttendanceDate = (DateTime)AttDate;
                StdAttend.ArrivalTime = AttTime;
                _context.Add(StdAttend);
                
            }
            else if (stat == "Leaving")
            {
                StdAttend = _context.Attendances.Where(a=>a.StudentId == id && a.AttendanceDate == (DateTime)AttDate).FirstOrDefault();

                StdAttend.LeaveTime = AttTime;
                _context.Update(StdAttend);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DepartmentStudents), new { DepartmentID = deptId, Status = stat });
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,AttendanceDate,ArrivalTime,LeaveTime")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,AttendanceDate,ArrivalTime,LeaveTime")] Attendance attendance)
        {
            if (id != attendance.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.StudentId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.StudentId == id);
        }
    }
}
