using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Matsoft.MongoDB;

public abstract class BaseContextDb
{
    public IMongoDatabase Database { get; }
    public MongoClient Client { get; }
    
    protected BaseContextDb(string connectionString, string databaseName)
    {
        try
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            Client = new MongoClient(settings);
            Database = Client.GetDatabase(databaseName);
            SetCamelCaseNameConvention();
            MapClasses();
        }
        catch (Exception ex)
        {
            throw new MongoException("Unable to connect to the database", ex);
        }
    }

    private void SetCamelCaseNameConvention()
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);
    }

    protected void RegisterMap<TEntity>() where TEntity : BaseEntity
    {
        BsonClassMap.RegisterClassMap<TEntity>(i =>
        {
            i.AutoMap();
            i.SetIgnoreExtraElements(true);
        });
    }

    protected abstract void MapClasses();
}