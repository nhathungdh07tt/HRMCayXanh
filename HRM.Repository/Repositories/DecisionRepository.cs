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
    public interface IDecisionRepository : IRepository<Decision>
    {
    }

    public class DecisionRepository : BaseRepository<Decision>, IDecisionRepository
    {
        public DecisionRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
