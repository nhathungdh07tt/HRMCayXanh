using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Department

{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
           
        }

        public long Id { get; set; }     
        public string Name { get; set; }
        public int Code { get; set; }

    }
}
