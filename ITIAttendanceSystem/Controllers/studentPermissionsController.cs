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
    public class studentPermissionsController : Controller
    {
        private readonly ITICOMPSYSDB2Context _context;

        public studentPermissionsController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }

        // GET: studentPermissions
        public async Task<IActionResult> Index()
        {
            var iTICOMPSYSDB2Context = _context.studentPermissions.Include(s => s.Instructor).Include(s => s.Student);
            return View(await iTICOMPSYSDB2Context.ToListAsync());
        }

        // GET: studentPermissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPermission = await _context.studentPermissions
                .Include(s => s.Instructor)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentPermission == null)
            {
                return NotFound();
            }

            return View(studentPermission);
        }

        // GET: studentPermissions/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: studentPermissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,PermissionDate,Note,PermissionState,PermissionType,InstructorId")] studentPermission studentPermission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentPermission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId", studentPermission.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentPermission.StudentId);
            return View(studentPermission);
        }

        // GET: studentPermissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPermission = await _context.studentPermissions.FindAsync(id);
            if (studentPermission == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId", studentPermission.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentPermission.StudentId);
            return View(studentPermission);
        }

        // POST: studentPermissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,PermissionDate,Note,PermissionState,PermissionType,InstructorId")] studentPermission studentPermission)
        {
            if (id != studentPermission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentPermission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!studentPermissionExists(studentPermission.Id))
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
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId", studentPermission.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentPermission.StudentId);
            return View(studentPermission);
        }

        // GET: studentPermissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentPermission = await _context.studentPermissions
                .Include(s => s.Instructor)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentPermission == null)
            {
                return NotFound();
            }

            return View(studentPermission);
        }

        // POST: studentPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentPermission = await _context.studentPermissions.FindAsync(id);
            _context.studentPermissions.Remove(studentPermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool studentPermissionExists(int id)
        {
            return _context.studentPermissions.Any(e => e.Id == id);
        }
    }
}
