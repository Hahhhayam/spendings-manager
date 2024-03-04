namespace BLL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private readonly Type _entityType;
        
        public EntityNotFoundException(Type entityName) 
            : base() 
        {
            _entityType = entityName;
        }

        public override string Message => $"Entity {_entityType.Name} not found. \n" + base.Message;
    }
}
