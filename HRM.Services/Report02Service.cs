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
    public interface IReport02Service : IService<Report02Model>
    {
        List<Report02ViewModel> GetReport();

    }

    public class Report02ModelService : BaseService<Report02Model>, IReport02Service
    {
        #region Properties

        private readonly IContext _context;
        private readonly IReport02Repository _repository;
     
        #endregion Properties

        #region Constructors

        public Report02ModelService(
            IContext context, 
            IReport02Repository repository
            ): base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public List<Report02ViewModel> GetReport()
        {
            var report_02 = _repository
                .GetAll().ToList();

            if (report_02 != null)
                return Mapper.Map<List<Report02Model>, List<Report02ViewModel>>(report_02);

            return new List<Report02ViewModel>();
        }

        #endregion Constructors


    }
}
