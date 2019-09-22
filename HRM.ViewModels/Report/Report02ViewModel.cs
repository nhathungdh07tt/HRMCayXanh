using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels.Report
{
    public class Report02ViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Bac1 { get; set; }
        public int Bac2 { get; set; }
        public int Bac3 { get; set; }
        public int Bac4 { get; set; }
        public int Bac5 { get; set; }
        public int Bac6 { get; set; }
        public int Bac7 { get; set; }
        public int Bac8 { get; set; }
        public int Tong => Bac1 + Bac2 + Bac3 + Bac4 + Bac5 + Bac6 + Bac7 + Bac8;
    }
}
