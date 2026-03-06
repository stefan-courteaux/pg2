namespace WebShoppie.Persistence;

public class OmgCustomerDoesNotExistInDBException(string? message) : Exception(message);