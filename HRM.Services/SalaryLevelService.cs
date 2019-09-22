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
using HRM.ViewModels.Salary;
using HRM.ViewModels.Work;

namespace HRM.Services
{
    public interface ISalaryLevelService : IService<SalaryLevel>
    {
        List<SalaryLevelViewModel> GetSalaryLevels();
        SalaryLevelViewModel GetInfo(long id);
        void Insert(SalaryLevelViewModel model);
        void Update(SalaryLevelViewModel model);
        void Delete(SalaryLevelViewModel model);
        List<SalaryLevelViewModel> GetSalaryLevelByWorkTitle(long worktitleId);
        
    }

    public class SalaryLevelService : BaseService<SalaryLevel>, ISalaryLevelService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ISalaryLevelRepository _repository;

        #endregion Properties

        #region Constructors

        public SalaryLevelService(IContext context, ISalaryLevelRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(SalaryLevelViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<WorkTitleViewModel> GetSalaryLevelByWorkTitle(long worktitleId)
        {
            var worktitles = _repository.GetAllByCondition(x => x.WorkTitle.Id == worktitleId).ToList();
            return AutoMapper.Mapper.Map<List<SalaryLevel>, List<WorkTitleViewModel>>(worktitles);
        }

        public List<SalaryLevelViewModel> GetSalaryLevels()
        {
            return _repository.GetAll().ProjectTo<SalaryLevelViewModel>().ToList();
        }

        public SalaryLevelViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<SalaryLevel, SalaryLevelViewModel>(contractType);

            return new SalaryLevelViewModel();
        }

        public void Insert(SalaryLevelViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SalaryLevelViewModel, SalaryLevel>(model);
            _repository.Add(contractType);
        }

        public void Update(SalaryLevelViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SalaryLevelViewModel, SalaryLevel>(model);
            _repository.Update(contractType);
        }

        List<SalaryLevelViewModel> ISalaryLevelService.GetSalaryLevelByWorkTitle(long worktitleId)
        {
            return _repository.GetAllByCondition(c=>c.WorkTitleId== worktitleId).ProjectTo<SalaryLevelViewModel>().ToList();
        }

        #endregion Constructors


    }
}