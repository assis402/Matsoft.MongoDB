using MongoDB.Driver;

namespace Matsoft.MongoDB;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task InsertOneAsync(TEntity entity, IClientSessionHandle session = null);
    
    public Task InsertManyAsync(IEnumerable<TEntity> entityList, IClientSessionHandle session = null);
    
    public Task InsertManyAsync(IClientSessionHandle session = null, params TEntity[] entityList);
    
    public Task<TEntity> FindByIdAsync(string id, IClientSessionHandle session = null);
    
    public Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null);

    public Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null);

    public Task<bool> Exists(FilterDefinition<TEntity> filterDefinition, IClientSessionHandle session = null);

    public Task UpdateOneAsync(TEntity entity, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session = null);

    public Task UpdateOneAsync(string id, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session = null);

    public Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition, IClientSessionHandle session = null);

    public Task DeleteOneAsync(string id, IClientSessionHandle session = null);
}