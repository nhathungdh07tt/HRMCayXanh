using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Department
{
    public class DepartmentEmployeeViewModel
    {
        public DepartmentEmployeeViewModel()
        {
           
        }

        public long Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

    }
}
