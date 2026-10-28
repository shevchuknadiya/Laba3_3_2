using Lab3_3_2.BLL.Models;
using Lab3_3_2.DAL;
using Lab3_3_2.DAL.Entities;
using Mapster;

namespace Lab3_3_2.BLL.EntityServices;

public abstract class EntityService<TEntity, TModel>
    where TEntity : PersonEntity
    where TModel : Person
{
    protected readonly EntityContext<TEntity> _entityContext;

    public EntityService(EntityContext<TEntity> entityContext)
    {
        _entityContext = entityContext;
    }

    public IReadOnlyList<TModel> GetAll()
    {
        var entities = _entityContext.Load();
        return entities.Select(e => e.Adapt<TModel>()).ToList().AsReadOnly();
    }

    public TModel GetAt(int index)
    {
        var entities = _entityContext.Load();
        return entities[index].Adapt<TModel>();
    }

    public void Add(TModel model)
    {
        var entity = model.Adapt<TEntity>();
        var entities = _entityContext.Load();
        entities.Add(entity);
        _entityContext.Save(entities);
    }

    public void RemoveAt(int index)
    {
        var entities = _entityContext.Load();
        entities.RemoveAt(index);
        _entityContext.Save(entities);
    }
}
