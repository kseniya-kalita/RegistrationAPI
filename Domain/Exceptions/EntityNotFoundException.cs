namespace Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName)
            : base($"Entity {entityName} cannot be found!")
        {

        }
    }
}
