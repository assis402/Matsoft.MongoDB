using MongoDB.Driver;

namespace Matsoft.MongoDB;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task InsertOneAsync(TEntity entity);
    
    public Task InsertAsync(IEnumerable<TEntity> entityList);
    
    public Task InsertManyAsync(params TEntity[] entityList);
    
    public Task<TEntity> FindByIdAsync(string id);
    
    public Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition);

    public Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition);

    public Task<bool> Exists(FilterDefinition<TEntity> filterDefinition);

    public Task UpdateOneAsync(TEntity entity, UpdateDefinition<TEntity> updateDefinition);

    public Task UpdateOneAsync(string id, UpdateDefinition<TEntity> updateDefinition);

    public Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition);

    public Task DeleteOneAsync(string id);
    
    public Task InsertOneAsync(TEntity entity, IClientSessionHandle session);
    
    public Task InsertAsync(IEnumerable<TEntity> entityList, IClientSessionHandle session);
    
    public Task InsertManyAsync(IClientSessionHandle session = null, params TEntity[] entityList);
    
    public Task<TEntity> FindByIdAsync(string id, IClientSessionHandle session);
    
    public Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session);

    public Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session);

    public Task<bool> Exists(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session);

    public Task UpdateOneAsync(TEntity entity, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session);

    public Task UpdateOneAsync(string id, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session);

    public Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session);

    public Task DeleteOneAsync(string id, IClientSessionHandle session);
}