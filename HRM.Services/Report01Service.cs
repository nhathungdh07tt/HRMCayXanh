using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Domain.ReportModel;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Report;
using HRM.ViewModels.System;

namespace HRM.Services
{
    public interface IReport01Service : IService<Report01Model>
    {
        List<Report01ViewModel> GetReport();

    }

    public class Report01ModelService : BaseService<Report01Model>, IReport01Service
    {
        #region Properties

        private readonly IContext _context;
        private readonly IReport01Repository _repository;
     
        #endregion Properties

        #region Constructors

        public Report01ModelService(
            IContext context, 
            IReport01Repository repository
            ): base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public List<Report01ViewModel> GetReport()
        {
            var report_01 = _repository
                .GetAll().ToList();

            if (report_01 != null)
                return Mapper.Map<List<Report01Model>, List<Report01ViewModel>>(report_01);

            return new List<Report01ViewModel>();
        }

        #endregion Constructors


    }
}
