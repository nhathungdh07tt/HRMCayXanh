namespace HRM.Domain.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}