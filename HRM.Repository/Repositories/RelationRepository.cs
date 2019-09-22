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
    public interface IRelationRepository : IRepository<Relation>
    {
    }

    public class RelationRepository : BaseRepository<Relation>, IRelationRepository
    {
        public RelationRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
