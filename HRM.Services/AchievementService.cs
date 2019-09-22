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
using HRM.ViewModels.Achievement;

namespace HRM.Services
{
    public interface IAchievementService : IService<Achievement>
    {
        List<AchievementViewModel> GetAchievements();
        AchievementViewModel GetInfo(long id);
        void Insert(AchievementViewModel model);
        void Update(AchievementViewModel model);
        void Delete(AchievementViewModel model);
        List<AchievementViewModel> GetAchievementByEmployee(long employeeId);
        
    }

    public class AchievementService : BaseService<Achievement>, IAchievementService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IAchievementRepository _repository;

        #endregion Properties

        #region Constructors

        public AchievementService(IContext context, IAchievementRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(AchievementViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<AchievementViewModel> GetAchievementByEmployee(long employeeId)        {
            
            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId, x => x.Employee)
                .ProjectTo<AchievementViewModel>().ToList();
        }

        public List<AchievementViewModel> GetAchievements()
        {
            return _repository.GetAll().ProjectTo<AchievementViewModel>().ToList();
        }

        public AchievementViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Achievement, AchievementViewModel>(contractType);

            return new AchievementViewModel();
        }

        public void Insert(AchievementViewModel model)
        {            
           var contractType = AutoMapper.Mapper.Map<AchievementViewModel, Achievement>(model);
            _repository.Add(contractType);
            Save();
        }

        public void Update(AchievementViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<AchievementViewModel, Achievement>(model);
            
            if (string.IsNullOrEmpty(contractType.Document))
            {
                var old = _repository.FindById(model.Id);
                if (old != null)
                    contractType.Document = old.Document;
            }

            _repository.Update(contractType);
            Save();
        }

        #endregion Constructors


    }
}