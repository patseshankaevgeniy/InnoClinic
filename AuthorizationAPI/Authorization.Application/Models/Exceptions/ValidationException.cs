namespace Authorization.Application.Models.Exceptions;

public class ValidationException(string message) : Exception(message);
