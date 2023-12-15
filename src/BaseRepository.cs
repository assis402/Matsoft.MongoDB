using Matsoft.MongoDB.Helpers;
using MongoDB.Driver;

namespace Matsoft.MongoDB;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _entityCollection;

    protected BaseRepository(BaseContextDb context)
        => _entityCollection = context?.Database?.GetCollection<TEntity>(name: Utils.GetCollectionName<TEntity>());
    
    public async Task InsertOneAsync(TEntity entity)
        => await _entityCollection.InsertOneAsync(document: entity);
    
    public async Task InsertAsync(IEnumerable<TEntity> entityList)
        => await _entityCollection.InsertManyAsync(entityList);
    
    public async Task InsertManyAsync(params TEntity[] entityList)
        => await _entityCollection.InsertManyAsync(entityList);
    
    public async Task<TEntity> FindByIdAsync(string id)
    {
        var filter = BaseEntity.FindByIdDefinition<TEntity>(id);
        return await FindOneAsync(filter); 
    }
    
    public async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition)
    {
        var result = await _entityCollection.FindAsync(filterDefinition);
        return await result.ToListAsync();
    }

    public async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition)
    {
        var result = await _entityCollection.FindAsync(filterDefinition);
        return await result.FirstOrDefaultAsync();
    }

    public async Task<bool> Exists(FilterDefinition<TEntity> filterDefinition)
    {
        var result = await _entityCollection.CountDocumentsAsync(filterDefinition);
        return result > 0;
    }

    public async Task UpdateOneAsync(TEntity entity,
        UpdateDefinition<TEntity> updateDefinition)
    {
        updateDefinition = entity.SetUpdateDateAndGetDefinition(updateDefinition);
        await _entityCollection.UpdateOneAsync(entity.FindByIdDefinition<TEntity>(), updateDefinition);
    }

    public async Task UpdateOneAsync(string id,
        UpdateDefinition<TEntity> updateDefinition)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(updateDefinition);
        await _entityCollection.UpdateOneAsync(BaseEntity.FindByIdDefinition<TEntity>(id), updateDefinition);
    }

    public async Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition,
        UpdateDefinition<TEntity> updateDefinition)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(updateDefinition);
        await _entityCollection.UpdateOneAsync(filterDefinition, updateDefinition);
    }

    public async Task DeleteOneAsync(string id)
    {
        var filterDefinition = BaseEntity.FindByIdDefinition<TEntity>(id);
        await _entityCollection.DeleteOneAsync(filterDefinition);
    }
    
    public virtual async Task InsertOneAsync(TEntity entity, IClientSessionHandle session)
        => await _entityCollection.InsertOneAsync(document: entity, session: session);
    
    public virtual async Task InsertAsync(IEnumerable<TEntity> entityList, IClientSessionHandle session)
        => await _entityCollection.InsertManyAsync(documents: entityList, session: session);
    
    public virtual async Task InsertManyAsync(IClientSessionHandle session, params TEntity[] entityList)
        => await _entityCollection.InsertManyAsync(documents: entityList, session: session);
    
    public virtual async Task<TEntity> FindByIdAsync(string id, IClientSessionHandle session)
    {
        var filter = BaseEntity.FindByIdDefinition<TEntity>(id: id);
        return await FindOneAsync(filterDefinition: filter, session: session); 
    }
    
    public virtual async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session)
    {
        var result = await _entityCollection.FindAsync(filter: filterDefinition, session: session);
        return await result.ToListAsync();
    }

    public virtual async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session)
    {
        var result = await _entityCollection.FindAsync(filter: filterDefinition, session: session);
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<bool> Exists(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session)
    {
        var result = await _entityCollection.CountDocumentsAsync(filter: filterDefinition, session: session);
        return result > 0;
    }

    public virtual async Task UpdateOneAsync(TEntity entity,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session)
    {
        updateDefinition = entity.SetUpdateDateAndGetDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: entity.FindByIdDefinition<TEntity>(), update: updateDefinition, session: session);
    }

    public virtual async Task UpdateOneAsync(string id,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: BaseEntity.FindByIdDefinition<TEntity>(id: id), update: updateDefinition, session: session);
    }

    public virtual async Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition,
        UpdateDefinition<TEntity> updateDefinition,
        IClientSessionHandle session)
    {
        updateDefinition = BaseEntity.UpdateDateDefinition(definition: updateDefinition);
        await _entityCollection.UpdateOneAsync(filter: filterDefinition, update: updateDefinition, session: session);
    }

    public virtual async Task DeleteOneAsync(string id, IClientSessionHandle session)
    {
        var filterDefinition = BaseEntity.FindByIdDefinition<TEntity>(id: id);
        await _entityCollection.DeleteOneAsync(filter: filterDefinition, session: session);
    }
}