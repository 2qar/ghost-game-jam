using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Find a way to ignore the player and the enemy layers at the same time

/* TODO: Finish raycasting for the enemy AI
 * - Make all of the values negative when the enemy is flipped so all of the rays are actually in the right spot
 * - Get the playermask info so the enemy can ignore that when they're shooting rays
 */

/// <summary>
/// Manages a bunch of enemy stuff, like what lines to say, and whether to use manually written lines or not.
/// </summary>
//[ExecuteInEditMode]
[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    public GhostMessage ghostMessage;
    static LayerMask walls;

    Animator animator;

    // enemy animations
    RuntimeAnimatorController enemy;
    RuntimeAnimatorController angryEnemy;

    private bool angery;
    public bool Angery
    {
        get { return angery; }
        set
        {
            if (value)
                animator.runtimeAnimatorController = angryEnemy;
            else
                animator.runtimeAnimatorController = enemy;

            angery = value;
        }
    }

    bool moveLeft;
    Vector2 movement;
    public float moveSpeed;
    Rigidbody2D rb;

    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        walls = 1 << LayerMask.NameToLayer("Platform");
        //player = ~player;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        enemy = Resources.Load<RuntimeAnimatorController>("Animations/Enemy");
        angryEnemy = Resources.Load<RuntimeAnimatorController>("Animations/AngryEnemy");

        sr = GetComponent<SpriteRenderer>();

        moveLeft = randomBoolValue();
        StartCoroutine(move());
	}
	
	// Update is called once per frame
	void Update ()
    {
        //rb.velocity = movement;

        if (angery)
            pursuitAI();
        else
            calmAI();
    }

    IEnumerator move()
    {
        if (moveLeft)
            movement.x = -moveSpeed;
        else
            movement.x = moveSpeed;
        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(3f);
        moveLeft = !moveLeft;
        move();
        yield break;
    }

    private bool randomBoolValue()
    {
        if (Random.Range(1, 3) > 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Does a bunch of raycasting so that the enemy doesn't stumble in chases
    /// </summary>
    private void pursuitAI()
    {
        
    }

    /// <summary>
    /// Bunch o raycasting for calm ghosts that like long walks on the beach and
    /// talking about their feelings
    /// </summary>
    private void calmAI()
    {
        Debug.Log(wallCheck());
    }

    private float highWall()
    {
        // min offset: (24, 16)
        float distance = 24;
        if (sr.flipX)
            distance *= -1;

        Vector3 offset = new Vector3(distance, 16);
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offset, Vector2.down, 4, walls);
        return ray.distance;
    }

    private float lowWall()
    {
        // min offset: (48, 8)
        Vector3 offset = new Vector3(48, 8);
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offset, Vector2.down, 4, walls);
        return ray.distance;
    }

    private bool wallCheck()
    {
        // minimum distance between the enemy and a wall of height 2 where the enemy can jump
        float distance = 20;

        Vector2 direction;
        if (!sr.flipX)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, walls);
        if (ray.collider != null)
            return true;
        Debug.Log(ray.collider);
        return false;
    }

    private bool pitCheck()
    {
        return false;
    }

    /*
    private bool platformCheck()
    {
        return false;
    }
    */

}
