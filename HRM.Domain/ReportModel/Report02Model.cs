using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.ReportModel
{
    public class Report02Model : Entity<long>
    {
        public string Name { get; set; }       
        public int Bac1 { get; set; }
        public int Bac2 { get; set; }
        public int Bac3 { get; set; }
        public int Bac4 { get; set; }
        public int Bac5 { get; set; }
        public int Bac6 { get; set; }
        public int Bac7 { get; set; }
        public int Bac8 { get; set; }
       

    }
}
