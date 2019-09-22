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
    public interface IDepartmentService : IService<Department>
    {
        List<DepartmentViewModel> GetDepartments();
        DepartmentViewModel GetInfo(long id);
        void Insert(DepartmentViewModel model);
        void Update(DepartmentViewModel model);
        void Delete(DepartmentViewModel model);
    }

    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDepartmentRepository _repository;

        #endregion Properties

        #region Constructors

        public DepartmentService(IContext context, IDepartmentRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DepartmentViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            return _repository.GetAll().ProjectTo<DepartmentViewModel>().ToList();
        }


        public DepartmentViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Department, DepartmentViewModel>(contractType);

            return new DepartmentViewModel();
        }

        public void Insert(DepartmentViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DepartmentViewModel, Department>(model);
            _repository.Add(contractType);
        }

        public void Update(DepartmentViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DepartmentViewModel, Department>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}