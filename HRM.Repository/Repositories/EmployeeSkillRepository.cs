using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.Repository.Base;
using HRM.Repository.Base.Interfaces;

namespace HRM.Repository.Repositories
{
    public interface IEmployeeSkillRepository : IRepository<EmployeeSkill>
    {

    }

    public class EmployeeSkillRepository : BaseRepository<EmployeeSkill>, IEmployeeSkillRepository
    {
        public EmployeeSkillRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}