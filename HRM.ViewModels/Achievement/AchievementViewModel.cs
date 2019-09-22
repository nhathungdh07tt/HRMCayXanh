﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Achievement
{
    public class AchievementViewModel
    {
        public AchievementViewModel()
        {
           
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public string Name { get; set; }      
        public DateTime? Date { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string Document { get; set; }
        public HttpPostedFileBase DocumentFile { get; set; }

    }
}
