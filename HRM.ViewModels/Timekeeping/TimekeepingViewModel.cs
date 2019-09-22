using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.ViewModels.Department;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Timekeeping
{
    public class TimekeepingViewModel
    {
        public TimekeepingViewModel()
        {
           
        }

        public long Id { get; set; }     
        public long EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }


        public long DepartmentId { get; set; }
        public virtual DepartmentViewModel Department { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }
        public string Note { get; set; }

        public string Document { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long TimeAttendanceTypeId { get; set; }
        public virtual TimeAttendanceType TimeAttendanceType { get; set; }

    }
}
