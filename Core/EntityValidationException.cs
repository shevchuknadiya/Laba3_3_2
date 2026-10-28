namespace Lab3_3_2.Core;

public class EntityValidationException : Exception
{
    public EntityValidationException(string message) : base(message) { }
}
