namespace Restaurant.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entity, object id)
        : base($"{entity} with ID '{id}' not found.")
    { }
}