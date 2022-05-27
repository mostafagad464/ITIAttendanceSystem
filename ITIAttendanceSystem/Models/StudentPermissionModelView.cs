
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query;

namespace ITIAttendanceSystem.Models
{
    public class StudentPermissionModelView
    {

       public IEnumerable<ITIAttendanceSystem.Models.studentPermission> list;
        
        public string SearchText { get; set; }
       // public SelectList Dates { get; set; }
        public DateTime SelectedDate { get; set; }
        public StudentPermissionModelView()
        {
            //List = null;
            list = null;
        }
    }
}
