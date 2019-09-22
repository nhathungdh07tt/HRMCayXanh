using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Education : Entity<long>
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
    }
}
