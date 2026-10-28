namespace Lab3_3_2.DAL.Entities;

public class StudentEntity : PersonEntity
{
    public required int Height { get; set; }
    public required int Weight { get; set; }
    public required string StudentIdCard { get; set; }
}
