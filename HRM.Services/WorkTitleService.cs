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
using HRM.ViewModels.Work;

namespace HRM.Services
{
    public interface IWorkTitleService : IService<WorkTitle>
    {
        List<WorkTitleViewModel> GetWorkTitles();
        WorkTitleViewModel GetInfo(long id);
        void Insert(WorkTitleViewModel model);
        void Update(WorkTitleViewModel model);
        void Delete(WorkTitleViewModel model);
    }

    public class WorkTitleService : BaseService<WorkTitle>, IWorkTitleService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IWorkTitleRepository _repository;

        #endregion Properties

        #region Constructors

        public WorkTitleService(IContext context, IWorkTitleRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(WorkTitleViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<WorkTitleViewModel> GetWorkTitles()
        {
            return _repository.GetAll().ProjectTo<WorkTitleViewModel>().ToList();
        }

        public WorkTitleViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<WorkTitle, WorkTitleViewModel>(contractType);

            return new WorkTitleViewModel();
        }

        public void Insert(WorkTitleViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkTitleViewModel, WorkTitle>(model);
            _repository.Add(contractType);
        }

        public void Update(WorkTitleViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkTitleViewModel, WorkTitle>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}