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
    public interface IReligionService : IService<Religion>
    {
        List<ReligionViewModel> GetReligions();
        ReligionViewModel GetInfo(long id);
        void Insert(ReligionViewModel model);
        void Update(ReligionViewModel model);
        void Delete(ReligionViewModel model);
    }

    public class ReligionService : BaseService<Religion>, IReligionService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IReligionRepository _repository;

        #endregion Properties

        #region Constructors

        public ReligionService(IContext context, IReligionRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(ReligionViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<ReligionViewModel> GetReligions()
        {
            return _repository.GetAll().ProjectTo<ReligionViewModel>().ToList();
        }

        public ReligionViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Religion, ReligionViewModel>(contractType);

            return new ReligionViewModel();
        }

        public void Insert(ReligionViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ReligionViewModel, Religion>(model);
            _repository.Add(contractType);
        }

        public void Update(ReligionViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ReligionViewModel, Religion>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}