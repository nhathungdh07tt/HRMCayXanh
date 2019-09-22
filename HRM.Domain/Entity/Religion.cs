using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Religion : Entity<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}