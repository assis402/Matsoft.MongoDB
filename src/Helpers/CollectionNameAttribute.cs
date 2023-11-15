namespace Matsoft.MongoDB.Helpers;

[AttributeUsage(AttributeTargets.Class)]
public class CollectionNameAttribute : Attribute 
{
    public string CollectionName { get; private set; }

    public CollectionNameAttribute(string collectionName) => CollectionName = collectionName;
}