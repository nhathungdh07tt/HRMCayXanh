using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Work
{
    public class WorkTitleDetailViewModel
    {
        public WorkTitleDetailViewModel()
        {
           
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Document { get; set; }
        public long WorkTitleId { get; set; }
        public virtual WorkTitleViewModel WorkTitle { get; set; }
        public HttpPostedFileBase DocumentFile { get; set; }
    }
}
