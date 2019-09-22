using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.Repository.Base;
using HRM.Repository.Base.Interfaces;

namespace HRM.Repository.Repositories
{
    public interface IWorkContractRepository : IRepository<WorkContract>
    {
    }

    public class WorkContractRepository : BaseRepository<WorkContract>, IWorkContractRepository
    {
        public WorkContractRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
