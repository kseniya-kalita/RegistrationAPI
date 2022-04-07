namespace Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
    }
}
