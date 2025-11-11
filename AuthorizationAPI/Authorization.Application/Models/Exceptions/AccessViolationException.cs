namespace Authorization.Application.Models.Exceptions;

public class AccessViolationException(string message) : Exception(message);
