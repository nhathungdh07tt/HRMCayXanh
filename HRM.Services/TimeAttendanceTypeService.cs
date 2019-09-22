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
using HRM.ViewModels.Timekeeping;

namespace HRM.Services
{
    public interface ITimeAttendanceTypeService : IService<TimeAttendanceType>
    {
        List<TimeAttendanceTypeViewModel> GetTimeAttendanceTypes();
        TimeAttendanceTypeViewModel GetInfo(long id);
        void Insert(TimeAttendanceTypeViewModel model);
        void Update(TimeAttendanceTypeViewModel model);
        void Delete(TimeAttendanceTypeViewModel model);
       
    }

    public class TimeAttendanceTypeService : BaseService<TimeAttendanceType>, ITimeAttendanceTypeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ITimeAttendanceTypeRepository _repository;

        #endregion Properties

        #region Constructors

        public TimeAttendanceTypeService(IContext context, ITimeAttendanceTypeRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(TimeAttendanceTypeViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<TimeAttendanceTypeViewModel> GetTimeAttendanceTypes()
        {
            return _repository.GetAll().ProjectTo<TimeAttendanceTypeViewModel>().ToList();
        }

       

        public TimeAttendanceTypeViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<TimeAttendanceType, TimeAttendanceTypeViewModel>(contractType);

            return new TimeAttendanceTypeViewModel();
        }

        public void Insert(TimeAttendanceTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<TimeAttendanceTypeViewModel, TimeAttendanceType>(model);
            _repository.Add(contractType);
        }

        public void Update(TimeAttendanceTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<TimeAttendanceTypeViewModel, TimeAttendanceType>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}