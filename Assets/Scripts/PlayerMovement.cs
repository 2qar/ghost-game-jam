using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Maybe make two seperate controllers for the ghost player and the human player

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    Rigidbody2D rb;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementController();
    }

    /// <summary>
    /// Get input from the player and apply it for movement.
    /// </summary>
    void movementController()
    {
        // get horizontal movement
        float xVelocity = Input.GetAxis("Horizontal");
        // check whether to flip the player's sprite or not
        flipPlayerSprite(xVelocity);
        // apply movement
        rb.velocity = new Vector2(xVelocity * moveSpeed, rb.velocity.y);

        bool grounded = checkIfGrounded("Player", 4, 4);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    /// <summary>
    /// Casts a ray towards the ground to check if the player is touching it; if the ray's distance is the same as half of the player's size, then the player is grounded.
    /// </summary>
    /// <param name="layerMask">Name of the player's layermask; used so it can be ignored.</param>
    /// <param name="halfPlayerSize">Half of the player object's size.</param>
    /// <param name="rayOffsetX">
    /// Offset on the x axis to cast the ray so the method can cast a ray on the left and right side of the player.
    /// </param>
    /// <returns>True if the player is grounded, false if not.</returns>
    bool checkIfGrounded(string layerMask, int halfPlayerSize, int rayOffsetX)
    {
        // set up a layermask so that the raycast can hit every layer except for the player
        LayerMask playerMask = 1 << LayerMask.NameToLayer(layerMask);
        playerMask = ~playerMask;

        // get the player's position with the given offset applied
        Vector2 playerLeftPosition = new Vector2(transform.position.x - rayOffsetX, transform.position.y);
        Vector2 playerRightPosition = new Vector2(transform.position.x + rayOffsetX, transform.position.y);

        // cast a ray on the left and right side of the player
        RaycastHit2D leftRay = Physics2D.Raycast(playerLeftPosition, Vector2.down, Mathf.Infinity, playerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(playerRightPosition, Vector2.down, Mathf.Infinity, playerMask);

        // if either of the rays' distances is equal to half of the player's size, then the player is touching the ground
        return Mathf.Round(leftRay.distance) == halfPlayerSize || Mathf.Round(rightRay.distance) == halfPlayerSize;
    }

    /// <summary>
    /// Flip the player's sprite based on their movement input.
    /// </summary>
    /// <param name="movement">
    /// Player movement input.
    /// </param>
    void flipPlayerSprite(float movement)
    {
        if (movement < 0)
            sr.flipX = true;
        else if(movement > 0)
            sr.flipX = false;
    }

}