using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    Rigidbody2D rb;

    bool jumping;
    float jumpTime = 3f;
    float currentJumpTime;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(xVelocity * moveSpeed, rb.velocity.y);

        // TODO: Better jump
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        jumping = true;
    }

}
