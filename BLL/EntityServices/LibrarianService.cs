using Lab3_3_2.BLL.Models;
using Lab3_3_2.DAL;
using Lab3_3_2.DAL.Entities;

namespace Lab3_3_2.BLL.EntityServices;

public class LibrarianService : EntityService<LibrarianEntity, Librarian>
{
    public LibrarianService(EntityContext<LibrarianEntity> entityContext) : base(entityContext)
    {
    }
}
