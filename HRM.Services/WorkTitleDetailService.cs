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
    public interface IWorkTitleDetailService : IService<WorkTitleDetail>
    {
        List<WorkTitleDetailViewModel> GetWorkTitleDetails();
        WorkTitleDetailViewModel GetInfo(long id);
        void Insert(WorkTitleDetailViewModel model);
        void Update(WorkTitleDetailViewModel model);
        void Delete(WorkTitleDetailViewModel model);
        List<WorkTitleDetailViewModel> GetWorkTitleDetailByWorkTitle(long worktitleId);
    }

    public class WorkTitleDetailService : BaseService<WorkTitleDetail>, IWorkTitleDetailService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IWorkTitleRepository _workTitleRepository;
        private readonly IWorkTitleDetailRepository _repository;

        #endregion Properties

        #region Constructors

        public WorkTitleDetailService(
            IContext context, 
            IWorkTitleDetailRepository repository,
            IWorkTitleRepository workTitleRepository
            ): base(context, repository)
        {
            _repository = repository;
            _context = context;
            _workTitleRepository = workTitleRepository;
        }

        public void Delete(WorkTitleDetailViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<WorkTitleDetailViewModel> GetWorkTitleDetailByWorkTitle(long workTitleId)
        {
            var workTitleDetails = _repository.GetAllByCondition(x => x.WorkTitle.Id == workTitleId).ToList();
            return AutoMapper.Mapper.Map<List<WorkTitleDetail>, List<WorkTitleDetailViewModel>>(workTitleDetails);
        }

        public List<WorkTitleDetailViewModel> GetWorkTitleDetails()
        {
            return _repository.GetAll(x => x.WorkTitle).ProjectTo<WorkTitleDetailViewModel>().ToList();
        }

        public WorkTitleDetailViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<WorkTitleDetail, WorkTitleDetailViewModel>(contractType);

            return new WorkTitleDetailViewModel();
        }

        public void Insert(WorkTitleDetailViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkTitleDetailViewModel, WorkTitleDetail>(model);
            _repository.Add(contractType);
        }

        public void Update(WorkTitleDetailViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkTitleDetailViewModel, WorkTitleDetail>(model);

            if (string.IsNullOrEmpty(contractType.Document))
            {
                var old = _repository.FindById(model.Id);
                if (old != null)
                    contractType.Document = old.Document;
            }

            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}