using MongoDB.Driver;

namespace Matsoft.MongoDB;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task InsertOneAsync(TEntity entity);
    
    public Task InsertManyAsync(IEnumerable<TEntity> entityList);
    
    public Task InsertManyAsync(params TEntity[] entityList);
    
    public Task<TEntity> FindByIdAsync(string id);
    
    public Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filterDefinition);

    public Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filterDefinition);

    public Task<bool> Exists(FilterDefinition<TEntity> filterDefinition);

    public Task UpdateOneAsync(TEntity entity, UpdateDefinition<TEntity> updateDefinition);

    public Task UpdateOneAsync(string id, UpdateDefinition<TEntity> updateDefinition);

    public Task UpdateOneAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition);

    public Task DeleteOneAsync(string id);
}