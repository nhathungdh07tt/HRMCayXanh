using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Department;

namespace HRM.Services
{
    public interface IDepartmentEmployeeService : IService<DepartmentEmployee>
    {
        List<DepartmentEmployeeViewModel> GetDepartmentEmployees();
        DepartmentEmployeeViewModel GetInfo(long id);
        void Insert(DepartmentEmployeeViewModel model);
        void Update(DepartmentEmployeeViewModel model);
        void Delete(DepartmentEmployeeViewModel model);
    }

    public class DepartmentEmployeeService : BaseService<DepartmentEmployee>, IDepartmentEmployeeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDepartmentEmployeeRepository _repository;

        #endregion Properties

        #region Constructors

        public DepartmentEmployeeService(IContext context, IDepartmentEmployeeRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DepartmentEmployeeViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<DepartmentEmployeeViewModel> GetDepartmentEmployees()
        {
            return _repository.GetAll().ProjectTo<DepartmentEmployeeViewModel>().ToList();
        }

        public DepartmentEmployeeViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<DepartmentEmployee, DepartmentEmployeeViewModel>(contractType);

            return new DepartmentEmployeeViewModel();
        }

        public void Insert(DepartmentEmployeeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DepartmentEmployeeViewModel, DepartmentEmployee>(model);
            _repository.Add(contractType);
        }

        public void Update(DepartmentEmployeeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DepartmentEmployeeViewModel, DepartmentEmployee>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}