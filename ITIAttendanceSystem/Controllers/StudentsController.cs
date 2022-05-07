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
    public class StudentsController : Controller
    {
        private readonly ITICOMPSYSDB2Context _context;

        public StudentsController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }

        // GET: Students
        public IActionResult Index(string selectdept)
        {
            
            var vm = new selectDepartmentViewModel();
            var depts= _context.Departments.Select(a => a.ShortName).ToList();
            vm.DepartmentSelectList=new SelectList(depts.Distinct().ToList());
            vm.students= _context.Students.Include(s => s.Department).ToList();
            vm.selectdept = selectdept;
            if (selectdept != null)
            {
                vm.students = vm.students.Where(a => a.Department != null && a.Department.ShortName == selectdept).ToList();

            }
            return View(vm);

        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentStatus,Id,DepartmentId,Address,Faculty,University,Specialization,GraduationYear,GraduationGrade,Mobile,HomePhone,MilitaryStatusName,Code")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentStatus,Id,DepartmentId,Address,Faculty,University,Specialization,GraduationYear,GraduationGrade,Mobile,HomePhone,MilitaryStatusName,Code")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        //Docs 
        [HttpGet]
        public async Task <IActionResult> Docs (Document doc,string selectedDept)
        {
            var document=_context.Documents.FirstOrDefault(a => a.StudentId == doc.StudentId);
            if(document==null)
            {
                _context.Documents.Add(doc);
            }
            else
            {
                _context.Documents.Remove(document);
                _context.Documents.Add(doc);

            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { selectdept = selectedDept });
            
            //selectDepartmentViewModel model = new selectDepartmentViewModel();
            //model.students = _context.Students.Include(a => a.Document).Where(a => a.StudentId == id).ToList();
            //return PartialView(model);
        }
    }
}


