namespace TwistedTaleweaver.Common.Extensions;

public static class ArrayExtensions
{
    public static T Random<T>(this T[] array)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (array.Length == 0)
        {
            throw new ArgumentException("Array cannot be empty.", nameof(array));
        }
        
        return array[System.Random.Shared.Next(0, array.Length)];
    }
}