using System.ComponentModel.DataAnnotations;

namespace HRM.Domain.Base
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}