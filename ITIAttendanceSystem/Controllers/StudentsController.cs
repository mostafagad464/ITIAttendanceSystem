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
using OfficeOpenXml;

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

        public IActionResult Index()
        {
            
            var vm = new selectDepartmentViewModel();
            var depts = _context.Departments.Select(a => a.ShortName).ToList();

            vm.DepartmentSelectList = new SelectList(depts.Distinct().ToList());
            vm.students = _context.Students.Include(s => s.Department).Include(s => s.Document).ToList();
            
            
            return View(vm);

        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id , int warning =0)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            if (warning == 1 || warning ==2 || warning==3)
            {
                Comment c = new Comment();
                if(warning == 1)
                    c.Body = "First Wearning";
                else if(warning == 2)
                    c.Body = "Second Wearning";
                else
                    c.Body = "separation";
                c.StudentId = (int)id;
                c.CommentType = warning;
                c.CommentDate = DateTime.Now;
                _context.Comments.Add(c);
                _context.SaveChanges();
            }

            var comments = _context.Comments.ToList();
            List<Comment> commentret = new List<Comment>();
            foreach(Comment comment in comments)
            {
                if (comment.StudentId == id)
                    commentret.Add(comment);
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.comment = commentret;
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var Grade = new List<string>();
            Grade.Add("Excellent");
            Grade.Add("Very Good");
            Grade.Add("Good");
            Grade.Add("Pass");

            var Military = new List<string>();
            Military.Add("None");
            Military.Add("Completed");
            Military.Add("Exempted");
            Military.Add("Finished");
            Military.Add("Postponed");

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "ShortName");
            ViewData["Grade"] = new SelectList(Grade);
            ViewData["Military"] = new SelectList(Military);
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
        //import excel sheet
        public async Task<IActionResult> Import(IFormFile file,[Bind("DepartmentId")] Student student)
        {
            int Deptid = (int)student.DepartmentId;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        student = new Student();
                        student.StudentStatus = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim());
                        student.Id = worksheet.Cells[row, 3].Value.ToString().Trim();
                        student.DepartmentId = Deptid;
                        student.Address = worksheet.Cells[row, 4].Value.ToString().Trim();
                        student.Faculty = worksheet.Cells[row, 5].Value.ToString().Trim();
                        student.University = worksheet.Cells[row, 6].Value.ToString().Trim();
                        student.Specialization = worksheet.Cells[row, 7].Value.ToString().Trim();
                        student.GraduationYear = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim());
                        student.GraduationGrade = worksheet.Cells[row, 9].Value.ToString().Trim();
                        student.Mobile = worksheet.Cells[row, 10].Value.ToString().Trim();
                        student.HomePhone = worksheet.Cells[row, 11].Value.ToString().Trim();
                        student.MilitaryStatusName = worksheet.Cells[row, 12].Value.ToString().Trim();
                        student.Code = worksheet.Cells[row, 13].Value.ToString().Trim();
                        student.StudentName = worksheet.Cells[row, 14].Value.ToString().Trim();
                        student.SecNo = int.Parse(worksheet.Cells[row, 15].Value.ToString().Trim());

                        _context.Add(student);
                        await _context.SaveChangesAsync();

                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }




        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var Grade = new List<string>();
            Grade.Add("Excellent");
            Grade.Add("Very Good");
            Grade.Add("Good");
            Grade.Add("Pass");

            var Military = new List<string>();
            Military.Add("None");
            Military.Add("Completed");
            Military.Add("Exempted");
            Military.Add("Finished");
            Military.Add("Postponed");
     

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
            ViewData["Grade"] = new SelectList(Grade);
            ViewData["Military"] = new SelectList(Military);
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
            return RedirectToAction(nameof(DeptStudents), new { DName = selectedDept });
            
            //selectDepartmentViewModel model = new selectDepartmentViewModel();
            //model.students = _context.Students.Include(a => a.Document).Where(a => a.StudentId == id).ToList();
            //return PartialView(model);
        }
        public IActionResult SendDataToDoc(int Stdid,string selectedDept)
        {
            return RedirectToAction(nameof(Index), new { selectdept = selectedDept , StudentId = Stdid });
        }
        public IActionResult DeptStudents(string DName, int? stdid)
        {
            var vm = new selectDepartmentViewModel();
            List<Student> students = _context.Students.Include(s => s.Department).Include(s => s.Document).ToList();
            if (DName == "All Departments") { vm.students = students; }
            else
            { vm.students = students.Where(a => a.Department != null && a.Department.ShortName == DName).ToList();}
            if (stdid != null)
                vm.StdId = (int)stdid;
            
            return PartialView(vm);
        }
    }
}


