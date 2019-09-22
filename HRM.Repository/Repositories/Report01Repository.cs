using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.ReportModel;
using HRM.Repository.Base;
using HRM.Repository.Base.Interfaces;

namespace HRM.Repository.Repositories
{
    public interface IReport01Repository : IRepository<Report01Model>
    {
    }

    public class Report01Repository : BaseRepository<Report01Model>, IReport01Repository
    {
        public Report01Repository(IContext dbFactory) 
            : base(dbFactory)
        {
      
        }
    }
}
