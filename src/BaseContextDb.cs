using System;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Matsoft.MongoDB;

public abstract class BaseContextDb
{
    public IMongoDatabase Database { get; }

    protected BaseContextDb(string connectionString, string databaseName)
    {
        try
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            var client = new MongoClient(settings);
            Database = client.GetDatabase(databaseName);
            MapClasses();
        }
        catch (Exception ex)
        {
            throw new MongoException("Não foi possível estabelecer a conexão ao banco de dados", ex);
        }
    }

    protected void MapClasses()
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);
    }
}