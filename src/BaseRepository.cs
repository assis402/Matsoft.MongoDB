using Matsoft.MongoDB.Helpers;
using MongoDB.Driver;

namespace Matsoft.MongoDB;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _entityCollection;

    protected BaseRepository(BaseContextDb context)
        => _entityCollection = context.Database.GetCollection<TEntity>(name: Utils.GetCollectionName<TEntity>());

    public async Task InsertOneAsync(TEntity entity, IClientSessionHandle session = null)
        => await _entityCollection.InsertOneAsync(document: entity, session: session);
    
    public async Task InsertManyAsync(IEnumerable<TEntity> entityList, IClientSessionHandle session = null)
        => await _entityCollection.InsertManyAsync(documents: entityList, session: session);
    
    public async Task InsertManyAsync(IClientSessionHandle session = null, params TEntity[] entityList)
        => await _entityCollection.InsertManyAsync(documents: entityList, session: session);
    
    public async Task<TEntity> FindByIdAsync(string id, IClientSessionHandle session = null)
    {
        var filter = BaseEntity.FindByIdDefinition<TEntity>(id: id);
        return await FindOneAsync(filterDefinition: filter, session: session); 
    }
    
    public async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null)
    {
        var result = await _entityCollection.FindAsync(filter: filterDefinition, session: session);
        return await result.ToListAsync();
    }

    public async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null)
    {
        var result = await _entityCollection.FindAsync(filter: filterDefinition, session: session);
        return await result.FirstOrDefaultAsync();
    }

    public async Task<bool> Exists(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null)
    {
        var result = await _entityCollection.CountDocumentsAsync(filter: filterDefinition, session: session);
        return result > 0;
    }

    public async Task UpdateOneAsync(TEntity entity,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session = null)
    {
        updateDefinition = entity.SetUpdateDateAndGetDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: entity.FindByIdDefinition<TEntity>(), update: updateDefinition, session: session);
    }

    public async Task UpdateOneAsync(string id,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session = null)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: BaseEntity.FindByIdDefinition<TEntity>(id: id), update: updateDefinition, session: session);
    }

    public async Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session = null)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: filterDefinition, update: updateDefinition, session: session);
    }

    public async Task DeleteOneAsync(string id, IClientSessionHandle session = null)
    {
        var filterDefinition = BaseEntity.FindByIdDefinition<TEntity>(id: id);
        await _entityCollection.DeleteOneAsync(filter: filterDefinition, session: session);
    }
}