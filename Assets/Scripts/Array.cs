/// <summary>
/// me shitty array methods
/// </summary>
public static class Array
{
    /// <summary>
    /// Fills an array
    /// </summary>
    /// <param name="array">Array to fill.</param>
    /// <param name="value">Value to fill it with.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    public static void fillArray<T>(this T[] array, T value)
    {
        for (int index = 0; index < array.Length; index++)
            array[index] = value;
    }

}