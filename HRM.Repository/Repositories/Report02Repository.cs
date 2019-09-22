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
    public interface IReport02Repository : IRepository<Report02Model>
    {
    }

    public class Report02Repository : BaseRepository<Report02Model>, IReport02Repository
    {
        public Report02Repository(IContext dbFactory) 
            : base(dbFactory)
        {
      
        }
    }
}
