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
    public interface IDecisionTypeRepository : IRepository<DecisionType>
    {
    }

    public class DecisionTypeRepository : BaseRepository<DecisionType>, IDecisionTypeRepository
    {
        public DecisionTypeRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
