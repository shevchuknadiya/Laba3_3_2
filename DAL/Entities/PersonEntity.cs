namespace Lab3_3_2.DAL.Entities;

public abstract class PersonEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Passport { get; set; }
}
