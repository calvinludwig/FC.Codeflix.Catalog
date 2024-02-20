namespace FC.Codeflix.Catalog.Application.Exceptions;

public class NotFoundException(string? message) : ApplicationException(message);
