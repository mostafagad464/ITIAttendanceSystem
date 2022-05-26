using ITIAttendanceSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace ITIAttendanceSystem.Controllers
{
    [Authorize(Roles = "StdAffairs")]
    public class DocumentController : Controller

    {
        private readonly ITICOMPSYSDB2Context _context;

        public DocumentController(ITICOMPSYSDB2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
