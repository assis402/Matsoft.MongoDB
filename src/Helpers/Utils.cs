using Matsoft.MongoDB.CustomAttributes;

namespace Matsoft.MongoDB.Helpers;

public static class Utils
{
    public static string FirstCharToLowerCase(this string @string)
        => char.ToLowerInvariant(@string[0]) + @string[1..];
    
    public static string GetCollectionName<TEntity>() where TEntity : class
    {
        var attribute = (CollectionNameAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(CollectionNameAttribute))!;
        return attribute?.CollectionName ?? string.Empty;
    }
}