using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Timekeeping
{
    public class TimeAttendanceTypeViewModel
    {
        public TimeAttendanceTypeViewModel()
        {
           
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal MonthlyBonus { get; set; }
        public string Content1 { get; set; }
        public string Note { get; set; }
        public string Document { get; set; }

    }
}
