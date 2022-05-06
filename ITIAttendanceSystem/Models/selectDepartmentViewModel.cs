using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Models
{
    public class selectDepartmentViewModel
    {
        public string selectdept { get; set; }
        public IEnumerable<ITIAttendanceSystem.Models.Student> students { get; set; }
        public SelectList DepartmentSelectList { get; set; }
    }
}
