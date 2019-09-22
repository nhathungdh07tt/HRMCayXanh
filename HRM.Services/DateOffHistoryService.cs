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
using HRM.ViewModels.Employee;

namespace HRM.Services
{
    public interface IDateOffHistoryService : IService<DateOffHistory>
    {
        List<DateOffHistoryViewModel> GetDateOffHistorys();
        DateOffHistoryViewModel GetInfo(long id);
        void Insert(DateOffHistoryViewModel model);
        void Update(DateOffHistoryViewModel model);
        void Delete(DateOffHistoryViewModel model);
        List<DateOffHistoryViewModel> GetDateOffHistoryByEmployee(long employeeId);
    }

    public class DateOffHistoryService : BaseService<DateOffHistory>, IDateOffHistoryService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDateOffHistoryRepository _repository;

        #endregion Properties

        #region Constructors

        public DateOffHistoryService(IContext context, IDateOffHistoryRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DateOffHistoryViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }
             

        public List<DateOffHistoryViewModel> GetDateOffHistoryByEmployee(long employeeId)
        {            
            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId, x => x.Employee)
                .ProjectTo<DateOffHistoryViewModel>().ToList();
        }

        public List<DateOffHistoryViewModel> GetDateOffHistorys()
        {
            return _repository.GetAll().ProjectTo<DateOffHistoryViewModel>().ToList();
        }

        public DateOffHistoryViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<DateOffHistory, DateOffHistoryViewModel>(contractType);

            return new DateOffHistoryViewModel();
        }

        public void Insert(DateOffHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DateOffHistoryViewModel, DateOffHistory>(model);
            _repository.Add(contractType);
        }

        public void Update(DateOffHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DateOffHistoryViewModel, DateOffHistory>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}