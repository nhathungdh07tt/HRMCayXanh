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
using HRM.ViewModels.Document;
using HRM.ViewModels.Timekeeping;

namespace HRM.Services
{
    public interface ITimekeepingService : IService<Timekeeping>
    {
        List<TimekeepingViewModel> GetTimekeepings();
        List<TimekeepingViewModel> Search(int? department, int? employeess, DateTime? Date);
        TimekeepingViewModel GetInfo(long id);
        void Insert(TimekeepingViewModel model);
        void Update(TimekeepingViewModel model);
        void Delete(TimekeepingViewModel model);
       
    }

    public class TimekeepingService : BaseService<Timekeeping>, ITimekeepingService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ITimekeepingRepository _repository;

        #endregion Properties

        #region Constructors

        public TimekeepingService(IContext context, ITimekeepingRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(TimekeepingViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<TimekeepingViewModel> GetTimekeepings()
        {
            return _repository.GetAll().ProjectTo<TimekeepingViewModel>().ToList();
        }

       

        public TimekeepingViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Timekeeping, TimekeepingViewModel>(contractType);

            return new TimekeepingViewModel();
        }

        public void Insert(TimekeepingViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<TimekeepingViewModel, Timekeeping>(model);
            _repository.Add(contractType);
        }

        public void Update(TimekeepingViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<TimekeepingViewModel, Timekeeping>(model);
            _repository.Update(contractType);
        }

        public List<TimekeepingViewModel> Search(int? department, int? employeess, DateTime? Date)
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (Date.HasValue)
                query = query.Where(x => x.Date == Date);           

            if (department > 0)
            {
                query = query.Where(x => x.DepartmentId == department);
            }

            if (employeess > 0)
            {
                query = query.Where(x => x.EmployeeId == employeess);
            }

            return query.ProjectTo<TimekeepingViewModel>().ToList();
                 
        }

        #endregion Constructors


    }
}