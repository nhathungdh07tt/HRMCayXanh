using HRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Entity
{
    public class Role : Entity<long>
    {
        public Role()
        {
               
        }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
