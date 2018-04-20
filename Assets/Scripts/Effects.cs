using UnityEngine;

/// <summary>
/// A collection of effects to use on GameObjects.
/// </summary>
public static class Effects
{
    /// <summary>
    /// Generate a random boolean value
    /// </summary>
    /// <returns>a randomly generated boolean von goolean</returns>
    public static bool randomBoolValue()
    {
        if (Random.Range(1, 3) > 1)
            return true;
        else
            return false;
    }
}