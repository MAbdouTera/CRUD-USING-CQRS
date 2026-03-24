namespace Application.Common.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"this {entity} with Id {id} not Found");