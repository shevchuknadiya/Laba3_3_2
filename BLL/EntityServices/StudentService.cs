using Lab3_3_2.BLL.Models;
using Lab3_3_2.DAL;
using Lab3_3_2.DAL.Entities;
using Mapster;

namespace Lab3_3_2.BLL.EntityServices;

public class StudentService : EntityService<StudentEntity, Student>
{
    public StudentService(EntityContext<StudentEntity> entityContext) : base(entityContext)
    {
    }

    public IReadOnlyList<Student> GetStudentsWithHealthyWeight()
    {
        var entities = _entityContext.Load();
        return entities.Where(s => s.Height - 110 == s.Weight).Select(s => s.Adapt<Student>()).ToList().AsReadOnly();
    }
}
