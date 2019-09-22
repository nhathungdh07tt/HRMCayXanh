using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;

namespace HRM.Services
{
    public interface IEmployeeSkillService : IService<EmployeeSkill>
    {

    }

    public class EmployeeSkillService : BaseService<EmployeeSkill>, IEmployeeSkillService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IEmployeeSkillRepository _repository;

        #endregion Properties

        #region Constructors

        public EmployeeSkillService(IContext context, IEmployeeSkillRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        #endregion Constructors

        
    }
}