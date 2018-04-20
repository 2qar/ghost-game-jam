using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE TO SELF: READONLY EXISTS
// FIXME: Ghost jumps up and bonks his head on a platform cus he thinks it's a wall
// TODO: Polish funky raycast AI values so the ghost seems less robot-y
// TODO: In the pursuit method, replace the player's position with the current position of the body being sought after

/// <summary>
/// Manages a bunch of enemy stuff, like what lines to say, and whether to use manually written lines or not.
/// </summary>
//[ExecuteInEditMode]
[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    const float speed = 15f;
    const float angrySpeed = 18f;

    public GhostMessage ghostMessage;
    static LayerMask walls;
    Raycaster caster;    

    public Animator animator { get; protected set; }

    // enemy animations
    RuntimeAnimatorController enemy;
    RuntimeAnimatorController angryEnemy;

    Vector2 playerPosition;

    private bool angery;
    public bool Angery
    {
        get { return angery; }
        set
        {
            StopAllCoroutines();
            if (value)
                // run towards the player w/ the animation
                updateEnemyState(angryEnemy, angrySpeed, pursuit());
            else
                // play the normal enemy walk and roam
                updateEnemyState(enemy, speed, walk());

            angery = value;
        }
    }

    // offsets for the enemy raycasts
    static Vector2 highWallOffset = new Vector2(19, 16);
    static Vector2 lowWallOffset = new Vector2(48, 8);
    static Vector2 platformOffset = new Vector2(-3, 0);

    bool _moveLeft;
    bool moveLeft
    {
        get { return _moveLeft; }
        set
        {
            // if the ghost actually flipped, flip all of the offsets
            if(_moveLeft != value)
            {
                caster.flipOffsets();
                moveSpeed *= -1;
            }
            
            _moveLeft = value;
        }
    }
    //Vector2 movement;
    public float moveSpeed = 15f;
    public Rigidbody2D rb { get; protected set; } 

    public SpriteRenderer sr { get; protected set; }

	// Use this for initialization
	void Start ()
    {
        getComponents();
        
        caster = new Raycaster(transform.position, highWallOffset, lowWallOffset, platformOffset);

        walls = 1 << LayerMask.NameToLayer("Platforms");

        moveLeft = Effects.randomBoolValue();
        Angery = false;
	}
	
    /// <summary>
    /// Get all of the references to the ghost's components.
    /// </summary>
    void getComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        enemy = Resources.Load<RuntimeAnimatorController>("Animations/Enemy");
        angryEnemy = Resources.Load<RuntimeAnimatorController>("Animations/AngryEnemy");
    }

    /// <summary>
    /// Update how the enemy is animated, how fast they move, and how they move.
    /// </summary>
    void updateEnemyState(RuntimeAnimatorController animation, float speed, IEnumerator movement)
    {
        animator.runtimeAnimatorController = animation;
        moveSpeed = getCorrectDirection(speed);
        StartCoroutine(movement);
    }

    /// <summary>
    /// Adjust speed so the ghost moves left or right depending on the direction they're facing.
    /// </summary>
    float getCorrectDirection(float speed)
    {
        if(moveLeft)
            return speed * -1;
        else
            return speed;
    }

    // TODO: Make the ghost walk a random distance or smth so he seems smart :)
    // also TODO: Make the ghost check for pits so he can stand on platforms
    /// <summary>
    /// make the lil ghosty boye walk BUT stop for walls in the way
    /// </summary>
    private IEnumerator walk()
    {
        // make the ghost face the correct direction and move until he finds a wall
        sr.flipX = moveLeft;
        while(!caster.wallCheck())
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            caster.updatePosition(transform.position);
            yield return null;
        }

        // stop the ghost and wait
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);

        // start moving but in the opposite direction
        moveLeft = !moveLeft;
        StartCoroutine(walk());
        yield break;
    }

    /// <summary>
    /// Chase the digga darn player!!!!
    /// </summary>
    private IEnumerator pursuit()
    {
        // loop to make the ghost chase the player 
        while(true)
        {
            // update the enemy's raycast position
            caster.updatePosition(transform.position);
            // get an updated version of the player's position
            playerPosition = GhostTalk.instance.transform.position;

            // move in the right direction based on where the player is at
            if (transform.position.x > playerPosition.x)
                sr.flipX = true;
            else
                sr.flipX = false;

            moveLeft = sr.flipX;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // SO if there's a wall in the way and the player isn't below the ghost
            // AND the wall is low enough AND there's not a platform, jump
            if (caster.highWallAndFloorCheck()
                && playerPosition.y > transform.position.y 
                && !caster.platformCheck()
                && Raycaster.checkIfGrounded(transform.position))
                rb.velocity = new Vector2(rb.velocity.x, 45);

            yield return null;
        }
    }

}
