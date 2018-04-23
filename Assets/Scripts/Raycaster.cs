using UnityEngine;

// TODO: Maybe hard-code offsets
/// <summary>
/// A bunch of raycasting stuff for AI and jumps and all sortsa things
/// </summary>
public class Raycaster 
{
	public const int halfPlayerSize = 4;
    private static LayerMask platforms = 1 << LayerMask.NameToLayer("Platforms");


    public Vector2 position { get; protected set; }

    public Vector2 highWallOffset { get; protected set; }
    public Vector2 lowWallOffset { get; protected set; }
    public Vector2 platformOffset { get; protected set; }

    /// <summary>
    /// Create a new Raycaster with a given enemy position and given offsets.
    /// </summary>
    public Raycaster(Vector2 position, Vector2 highWallOffset, Vector2 lowWallOffset, Vector2 platformOffset)
    {
        this.position = position;
        this.highWallOffset = highWallOffset;
        this.lowWallOffset = lowWallOffset;
        this.platformOffset = platformOffset;
    }

    /// <summary>
    /// Update the position to shoot a raycast from.
    /// Put this in update or something like that so the object
    /// using a raycaster always has an updated position to cast from.
    /// </summary>
    public void updatePosition(Vector2 position) { this.position = position; }

    /// <summary>
    /// Flip the offsets when the object casting rays flips.
    /// </summary>
    public void flipOffsets()
    {
        highWallOffset = new Vector2(highWallOffset.x * -1, highWallOffset.y);
        lowWallOffset = new Vector2(lowWallOffset.x * -1, lowWallOffset.y);
        platformOffset = new Vector2(platformOffset.x * -1, platformOffset.y);
    }

    /// <summary>
    /// Casts a ray towards the ground to check if the player is touching it; 
    /// if the ray's distance is the same as half of the player's size,
    /// then the player is grounded.
    /// </summary>
    /// <returns>True if the player is grounded, false if not.</returns>
    public static bool checkIfGrounded(Vector2 position)
    {
        // get the player's position with the given offset applied
        Vector2 playerLeftPosition = new Vector2(position.x - halfPlayerSize, position.y);
        Vector2 playerRightPosition = new Vector2(position.x + halfPlayerSize, position.y);

        // cast a ray on the left and right side of the player
        RaycastHit2D leftRay = Physics2D.Raycast(playerLeftPosition, Vector2.down, Mathf.Infinity, platforms);
        RaycastHit2D rightRay = Physics2D.Raycast(playerRightPosition, Vector2.down, Mathf.Infinity, platforms);

        // if either of the rays' distances is equal to half of the player's size, then the player is touching the ground
        return Mathf.Round(leftRay.distance) == halfPlayerSize || Mathf.Round(rightRay.distance) == halfPlayerSize;
    }

    /// <summary>
    /// does a lil raycast in a spot where a high wall would be
    /// </summary>
    /// <returns>is there a wall????</returns>
    private RaycastHit2D highWallCast()
    {
        // shoot a ray down to check for a wall
        RaycastHit2D ray = Physics2D.Raycast(position + highWallOffset, Vector2.down, 50, platforms);
        return ray;
    }

    /// <summary>
    /// Check for a wall for the ghost to jump over
    /// </summary>
    /// <returns>is there a wall for the ghost to jump over?</returns>
    public bool highWallAndFloorCheck()
    {
        RaycastHit2D ray = highWallCast();
        float distance = ray.distance;

        // if the distance is 0, the wall is too high
        // if the distance is 20, the ray is hitting the floor
        // if the distance is in between, there's a jumpable wall
        if (distance > 0 && distance < 19)
            return true;

        return false;
    }

    /// <summary>
    /// checks for a smol woll for the ghosty boye to hop up on all scronched-like
    /// </summary>
    /// <returns>is there a tiny lil baby wall????</returns>
    public RaycastHit2D lowWall()
    {
        RaycastHit2D ray = Physics2D.Raycast(position + lowWallOffset, Vector2.down, 4, platforms);
        return ray;
    }

    /// <summary>
    /// checks if there's a big ol wall in front of the ghosty boye to avoid hitting
    /// </summary>
    /// <returns>is there a wall????</returns>
    public bool wallCheck()
    {
        float distance = Mathf.Round(highWallCast().distance);

        // if distance is 20 it's hitting the floor or a pit
        if (distance >= 19)
            return false;
        
        return true;
    }

    // FIXME: Hitting the enemy or something cus it's returning 0 a lot
    /// <summary>
    /// Check for a platform above the ghost's head.
    /// </summary>
    /// <returns>is there a lil platform above our ghosty boye's head?</returns>
    public bool platformCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(position + platformOffset, Vector2.up, 16, platforms);

        if (Mathf.Round(ray.distance) >= 12)
            return true;

        return false;
    }

}
