using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.ReportModel
{
    public class Report01Model : Entity<long>
    {
        public string Name { get; set; }
        public int tong { get; set; }
        public int nam { get; set; }
        public int nu { get; set; }
        public int thuviec { get; set; }
        public int thang6 { get; set; }
        public int thang12 { get; set; }
        public int thang24 { get; set; }
        public int thang36 { get; set; }
        public int kxd { get; set; }
        public int daihoc { get; set; }
        public int caodang { get; set; }
        public int trungcap { get; set; }
        public int socapnghe { get; set; }
        public int dttc { get; set; }

    }
}
