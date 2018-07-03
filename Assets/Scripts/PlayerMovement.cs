using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private MovementAnimation movementAnimation;

    public float moveSpeed;
    public float jumpHeight;

    public Rigidbody2D rb { get; protected set; }
    public SpriteRenderer sr { get; protected set; }

    // Use this for initialization
    void Start()
    {
        instance = this;

        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        movementAnimation = GetComponent<MovementAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        ghostMovementController();
    }

    /// <summary>
    /// Get input from the player and apply it for movement.
    /// </summary>
    void ghostMovementController()
    {
        // get horizontal movement
        float xVelocity = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            movementAnimation.pressingMovementKey = true;
        else
            movementAnimation.pressingMovementKey = false;
            

        // apply movement
        rb.velocity = new Vector2(xVelocity * moveSpeed, rb.velocity.y);

        bool grounded = Raycaster.checkIfGrounded(transform.position);
        movementAnimation.grounded = grounded;

        // jump if the player is on the ground and presses the space bar
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void playerMovementController()
    {
        // get horizontal movement
        float xVelocity = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            movementAnimation.pressingMovementKey = true;
            if(Input.GetKey(KeyCode.A))
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            movementAnimation.pressingMovementKey = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        bool grounded = Raycaster.checkIfGrounded(transform.position);
        movementAnimation.grounded = grounded;

        // jump if the player is on the ground and presses the space bar
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

}

