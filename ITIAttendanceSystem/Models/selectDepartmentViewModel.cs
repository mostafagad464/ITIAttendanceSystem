using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Models
{
    public class selectDepartmentViewModel
    {
        public string selectdept { get; set; }
        public IEnumerable<ITIAttendanceSystem.Models.Student> students { get; set; }
        //public Document document { get; set; }
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int show { get; set; }
        public SelectList DepartmentSelectList { get; set; }
    }
}
