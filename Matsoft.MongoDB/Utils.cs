namespace Matsoft.MongoDB;

public static class Utils
{
    public static string FirstCharToLowerCase(this string @string)
        => char.ToLowerInvariant(@string[0]) + @string[1..];
}