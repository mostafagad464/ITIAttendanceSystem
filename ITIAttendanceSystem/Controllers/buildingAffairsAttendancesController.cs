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
    [Authorize(Roles = "Security")]
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
            return View();
        }
        public async Task<IActionResult> Staff(string Status)
        {
            List<BuildingAffairsStaff> StaffsList = new List<BuildingAffairsStaff>();

            List<BuildingAffairsStaff> AllStaff = _context.BuildingAffairsStaffs.ToList();

            DateTime today = DateTime.Today;
            //Staff attended
            List<buildingAffairsAttendance> Attended = _context.buildingAffairsAttendances.Include(s => s.Staff).Where(a => a.AttendanceDate == today && a.LeaveTime == null).ToList();
            List<BuildingAffairsStaff> StaffsAttended = new List<BuildingAffairsStaff>();

            foreach (buildingAffairsAttendance attendance in Attended)
            {
                StaffsAttended.Add(attendance.Staff);
            }

            //Staff attended and left
            List<buildingAffairsAttendance> Left = _context.buildingAffairsAttendances.Include(s => s.Staff).Where(a => a.AttendanceDate == today && a.LeaveTime != null).ToList();
            List<BuildingAffairsStaff> StaffsLeft = new List<BuildingAffairsStaff>();

            foreach (buildingAffairsAttendance attendance in Left)
            {
                StaffsLeft.Add(attendance.Staff);
            }

            List<BuildingAffairsStaff> StaffsNotAttend = AllStaff.Except(StaffsAttended).ToList();
            StaffsNotAttend = StaffsNotAttend.Except(StaffsLeft).ToList();

            ViewBag.Status = Status;
            if (Status == "Arriving")
            {
                StaffsList = StaffsNotAttend;
            }
            else if (Status == "Leaving")
            {
                StaffsList = StaffsAttended;
            }
            return PartialView(StaffsList);
        }
        public async Task<IActionResult> Attend(int id, string stat)
        {
            buildingAffairsAttendance StfAttend;

            if (stat == "Arriving")
            {
                StfAttend = new buildingAffairsAttendance();
                StfAttend.StaffId = id;
                StfAttend.AttendanceDate = DateTime.Today;
                StfAttend.ArrivalTime = DateTime.Now.TimeOfDay;
                _context.Add(StfAttend);

            }
            else if (stat == "Leaving")
            {
                StfAttend = _context.buildingAffairsAttendances.Where(a => a.StaffId == id && a.AttendanceDate == DateTime.Today).FirstOrDefault();

                StfAttend.LeaveTime = DateTime.Now.TimeOfDay;
                _context.Update(StfAttend);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Staff), new { Status = stat });
        }

        private bool buildingAffairsAttendanceExists(int id)
        {
            return _context.buildingAffairsAttendances.Any(e => e.StaffId == id);
        }
    }
}
