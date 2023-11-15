using System;
using Matsoft.MongoDB.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Matsoft.MongoDB;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = ObjectId.GenerateNewId();
        CreatedDate = DateTime.UtcNow;
    }

    [BsonId] 
    public ObjectId Id { get; private set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    #region Definitions
    
    public FilterDefinition<TEntity> FindByIdDefinition<TEntity>()
    {
        return Builders<TEntity>.Filter.Eq("_id", Id);
    }

    public static FilterDefinition<TEntity> FindByIdDefinition<TEntity>(string id)
    {
        return Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
    }

    public UpdateDefinition<TEntity> SetUpdateDateAndGetDefinition<TEntity>(UpdateDefinition<TEntity> definition)
    {
        UpdateDate = DateTime.UtcNow;
        return definition.Set(nameof(UpdateDate).FirstCharToLowerCase(), UpdateDate);
    }
    
    public static UpdateDefinition<TEntity> UpdateDateDefinition<TEntity>(UpdateDefinition<TEntity> definition) 
        => definition.Set(nameof(UpdateDate).FirstCharToLowerCase(), DateTime.UtcNow);

    #endregion
}