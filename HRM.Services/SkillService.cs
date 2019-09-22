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
    public interface ISkillService : IService<Skill>
    {
        List<SkillViewModel> GetSkills();
        SkillViewModel GetInfo(long id);
        void Insert(SkillViewModel model);
        void Update(SkillViewModel model);
        void Delete(SkillViewModel model);
    }

    public class SkillService : BaseService<Skill>, ISkillService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ISkillRepository _repository;

        #endregion Properties

        #region Constructors

        public SkillService(IContext context, ISkillRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(SkillViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<SkillViewModel> GetSkills()
        {
            return _repository.GetAll().ProjectTo<SkillViewModel>().ToList();
        }

        public SkillViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Skill, SkillViewModel>(contractType);

            return new SkillViewModel();
        }

        public void Insert(SkillViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SkillViewModel, Skill>(model);
            _repository.Add(contractType);
        }

        public void Update(SkillViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SkillViewModel, Skill>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}