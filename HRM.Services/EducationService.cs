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
    public interface IEducationService : IService<Education>
    {
        List<EducationViewModel> GetEducations();
        EducationViewModel GetInfo(long id);
        void Insert(EducationViewModel model);
        void Update(EducationViewModel model);
        void Delete(EducationViewModel model);
    }

    public class EducationService : BaseService<Education>, IEducationService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IEducationRepository _repository;

        #endregion Properties

        #region Constructors

        public EducationService(IContext context, IEducationRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(EducationViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<EducationViewModel> GetEducations()
        {
            return _repository.GetAll().ProjectTo<EducationViewModel>().ToList();
        }

        public EducationViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Education, EducationViewModel>(contractType);

            return new EducationViewModel();
        }

        public void Insert(EducationViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<EducationViewModel, Education>(model);
            _repository.Add(contractType);
        }

        public void Update(EducationViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<EducationViewModel, Education>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}