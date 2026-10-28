namespace Lab3_3_2.BLL.Models;

public class Librarian : Person
{
    public required string Department { get; set; }

    protected override string ToStringImp() => $"Librarian {FullName}. Passport: {Passport}, Department: {Department}";
}
