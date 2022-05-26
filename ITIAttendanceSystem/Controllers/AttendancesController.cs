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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Security")]
        public IActionResult Index()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }
        // GET: Attendances/IndexOnline
        [Authorize(Roles = "StdAffairs")]
        public IActionResult IndexOnline()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }

        //Attendance: /Attendances/BarCode
        //Leaving : /Attendances/BarCode?stat=L
        [Authorize(Roles = "Security")] 
        public IActionResult BarCode(string? stat)
        {
            int att = 0;
            if (stat == null)
            {
                ViewBag.Header = "Atttendance Bar Code";
                att = 1;
            }
            else if(stat == "L")
            {
                ViewBag.Header = "Checkout Bar Code";
            }
            return View(att);
        }

        [Authorize(Roles = "StdAffairs,Security")]
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

            List<Attendance> Left = _context.Attendances.Include(s => s.Student).Where(a => a.AttendanceDate == today && a.Student.DepartmentId == DepartmentID && a.LeaveTime != null).ToList();
            List<Student> StudentsLeft = new List<Student>();

            foreach (Attendance attendance in Left)
            {
                StudentsLeft.Add(attendance.Student);
            }

            List<Student> StudentsNotAttend = StudentsInDept.Except(StudentsAttended).ToList();
            StudentsNotAttend = StudentsNotAttend.Except(StudentsLeft).ToList();

            ViewBag.Status = Status;

            if (Status == "Leaving")
            {
                studentsList = StudentsAttended;
            }
            else if (Status == "Arriving")
            {
                studentsList = StudentsNotAttend;
            }
            else if(Status == "Left")
            {
                studentsList = StudentsLeft;
            }
            return PartialView(studentsList);
        }
        
        [Authorize(Roles = "StdAffairs,Security")]
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

        [Authorize(Roles = "Security")]
        public async Task<IActionResult> BarCodeAttend(string BCode)
        {
            Student std = _context.Students.FirstOrDefault(a => a.Code == BCode);

            Attendance StdAttend;
            
            if (std != null)
            {
                StdAttend = _context.Attendances.Where(a => a.StudentId == std.StudentId && a.AttendanceDate == DateTime.Today).FirstOrDefault();
                
                if (StdAttend == null)
                {
                    StdAttend = new Attendance();
                    StdAttend.StudentId = std.StudentId;
                    StdAttend.AttendanceDate = DateTime.Today;
                    StdAttend.ArrivalTime = DateTime.Now.TimeOfDay;
                    _context.Add(StdAttend);
                    await _context.SaveChangesAsync();
                }

                //To view the students who already attended, we will set the status to "Leaving"
                return RedirectToAction(nameof(DepartmentStudents), new { DepartmentID = std.DepartmentId, Status = "Leaving" });
            }
            else
            {
                return Content("Not found");
            }
        }

        [Authorize(Roles = "Security")]
        public async Task<IActionResult> BarCodeLeave(string BCode)
        {
            Student std = _context.Students.FirstOrDefault(a => a.Code == BCode);

            Attendance StdAttend;

            if (std != null)
            {
                StdAttend = _context.Attendances.Where(a => a.StudentId == std.StudentId && a.AttendanceDate == DateTime.Today && a.LeaveTime == null).FirstOrDefault();

                if (StdAttend != null)
                {
                    StdAttend.LeaveTime = DateTime.Now.TimeOfDay;
                    _context.Update(StdAttend);
                    await _context.SaveChangesAsync();
                }
                else if(StdAttend ==null)
                {
                    StdAttend = _context.Attendances.Where(a => a.StudentId == std.StudentId && a.AttendanceDate == DateTime.Today && a.LeaveTime != null).FirstOrDefault();
                    if (StdAttend == null)
                    {
                        return Content("Didn't attend");
                    }
                }
                //To view the students who already attended, we will set the status to "Leaving"
                return RedirectToAction(nameof(DepartmentStudents), new { DepartmentID = std.DepartmentId, Status = "Left" });
            }
            else
            {
                return Content("Not found");
            }
        }
        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.StudentId == id);
        }
    }
}
