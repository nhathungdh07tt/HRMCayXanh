using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class WorkTitleDetail : Entity<long>
    {
        public WorkTitleDetail()
        {
            
        }

        [ForeignKey(nameof(WorkTitle))]
        public long WorkTitleId { get; set; }
        public virtual WorkTitle WorkTitle { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Document { get; set; }
    }
}
