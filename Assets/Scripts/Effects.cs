using UnityEngine;

/// <summary>
/// A collection of effects to use on GameObjects.
/// </summary>
public static class Effects
{
    const int halfPlayerSize = 4;

    /// <summary>
    /// Casts a ray towards the ground to check if the player is touching it; 
    /// if the ray's distance is the same as half of the player's size,
    /// then the player is grounded.
    /// </summary>
    /// <returns>True if the player is grounded, false if not.</returns>
    public static bool checkIfGrounded(Vector2 position)
    {
        // set up a layermask so the ray can only hit platforms
        LayerMask playerMask = 1 << LayerMask.NameToLayer("Platforms");

        // get the player's position with the given offset applied
        Vector2 playerLeftPosition = new Vector2(position.x - halfPlayerSize, position.y);
        Vector2 playerRightPosition = new Vector2(position.x + halfPlayerSize, position.y);

        // cast a ray on the left and right side of the player
        RaycastHit2D leftRay = Physics2D.Raycast(playerLeftPosition, Vector2.down, Mathf.Infinity, playerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(playerRightPosition, Vector2.down, Mathf.Infinity, playerMask);

        // if either of the rays' distances is equal to half of the player's size, then the player is touching the ground
        return Mathf.Round(leftRay.distance) == halfPlayerSize || Mathf.Round(rightRay.distance) == halfPlayerSize;
    }

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