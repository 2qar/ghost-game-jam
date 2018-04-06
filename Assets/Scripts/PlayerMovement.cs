using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Maybe make two seperate controllers for the ghost player and the human player

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed;
    public float jumpHeight;

    Rigidbody2D rb;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        instance = this;

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

        bool grounded = Effects.checkIfGrounded(transform.position);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
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