using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Achievement
{
    public class AchievementCollectionViewModel
    {
        public AchievementCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            Archievements = new List<AchievementViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<AchievementViewModel> Archievements { get; set; }
    }
}
