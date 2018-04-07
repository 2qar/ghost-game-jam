using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE TO SELF: READONLY EXISTS
// FIXME: Ghost sometimes starts facing the wrong direction which breaks the whole pursuit thingy
// FIXME: Ghost sometimes clips high walls when he jumps and doesn't make it
// FIXME: Ghost jumps up and bonks his head on a platform cus he thinks it's a wall
// TODO: Polish funky raycast AI values so the ghost seems less robot-y

/// <summary>
/// Manages a bunch of enemy stuff, like what lines to say, and whether to use manually written lines or not.
/// </summary>
//[ExecuteInEditMode]
[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    public GhostMessage ghostMessage;
    static LayerMask walls;
    //static LayerMask enemyMask;

    Animator animator;

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
                // play the angry boy animation and chase the player
                StartCoroutine(pursuit());
            else
                // play the normal enemy walk and roam
                StartCoroutine(walk());

            angery = value;
        }
    }

    static Vector3[] offsets = { new Vector3(20, 16), new Vector3(48, 8), new Vector3(-3, 0) };
    enum Offsets { Highwall, LowWall, Platform }

    bool _moveLeft;
    bool moveLeft
    {
        get { return _moveLeft; }
        set
        {
            // if the ghost actually flipped, flip all of the offsets
            if(_moveLeft != value)
            {
                for (int index = 0; index < offsets.Length; index++)
                    offsets[index].x *= -1;
                moveSpeed *= -1;
            }
            
            _moveLeft = value;
        }
    }
    //Vector2 movement;
    public float moveSpeed = 15f;
    Rigidbody2D rb;

    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        getComponents();

        walls = 1 << LayerMask.NameToLayer("Platforms");
        //enemyMask = 1 << LayerMask.NameToLayer("Frenemy");

        // move in a random direction
        moveLeft = Effects.randomBoolValue();
        StartCoroutine(walk());
        Angery = true;
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

	// Update is called once per frame
	void Update ()
    {
        
    }

    /// <summary>
    /// does a lil raycast in a spot where a high wall would be
    /// </summary>
    /// <returns>is there a wall????</returns>
    private RaycastHit2D highWallCast()
    {
        // shoot a ray down to check for a wall
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offsets[(int)Offsets.Highwall], Vector2.down, 50, walls);
        return ray;
    }

    /// <summary>
    /// Check for a wall for the ghost to jump over
    /// </summary>
    /// <returns>is there a wall for the ghost to jump over?</returns>
    private bool highWallAndFloorCheck()
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
    private RaycastHit2D lowWall()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offsets[(int)Offsets.LowWall], Vector2.down, 4, walls);
        return ray;
    }

    /// <summary>
    /// checks if there's a big ol wall in front of the ghosty boye to avoid hitting
    /// </summary>
    /// <returns>is there a wall????</returns>
    private bool wallCheck()
    {
        // minimum distance between the enemy and a wall of height 2 where the enemy can jump
        //float distance = 20;

        float distance = Mathf.Round(highWallCast().distance);

        // if distance is 20 it's hitting the floor or a pit
        if (distance >= 19)
            return false;
        
        return true;
    }

    // TODO: Make the ghost walk a random distance or smth so he seems smart :)
    /// <summary>
    /// make the lil ghosty boye walk BUT stop for walls in the way
    /// </summary>
    private IEnumerator walk()
    {
        // play the normal walking animation
        animator.runtimeAnimatorController = enemy;

        // move at a calm pace
        moveSpeed = 15f;

        // make the ghost face the correct direction and move until he finds a wall
        sr.flipX = moveLeft;
        while(!wallCheck())
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
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

    // FIXME: Ghost kinda sorta spazzes out when they finally make it to the player
        // maybe this won't really be an issue because the ghost won't ever be
        // just standing at the player's position, by that point the player is 
        // gonna get smacked and the enemy is gonna stop being angry
    /// <summary>
    /// Chase the digga darn player!!!!
    /// </summary>
    private IEnumerator pursuit()
    {
        // play the angry "im-gonna-getcha" animation oooo scary!!!
        animator.runtimeAnimatorController = angryEnemy;

        // move at an aggressive pace
        moveSpeed = 18f;
        sr.flipX = moveLeft;

        while(true)
        {
            // get an updated version of the player's position
            playerPosition = GhostTalk.instance.transform.position;

            // move in the right direction based on where the player is at
            if (transform.position.x > playerPosition.x)
                sr.flipX = true;
            else
                sr.flipX = false;

            moveLeft = sr.flipX;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // OK so this implementation of making the enemy jump over walls
            // isn't really what i had in mind but it works and if i make it
            // so the ghost won't jump if the wall is too high it would make this
            // kinda perfect pretty much :)

            // SO if there's a wall in the way and the player isn't below the ghost
            // AND the wall is low enough AND there's not a platform, jump
            if (highWallAndFloorCheck() 
                && playerPosition.y > transform.position.y 
                && !platformCheck() 
                && Effects.checkIfGrounded(transform.position))
                rb.velocity = new Vector2(rb.velocity.x, 45);

            yield return null;
        }
    }

    // FIXME: Hitting the enemy or something cus it's returning 0 a lot
    /// <summary>
    /// Check for a platform above the ghost's head.
    /// </summary>
    /// <returns>is there a lil platform above our ghosty boye's head?</returns>
    private bool platformCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offsets[(int)Offsets.Platform], Vector2.up, 16, walls);

        if (Mathf.Round(ray.distance) >= 12)
            return true;

        return false;
    }

}
