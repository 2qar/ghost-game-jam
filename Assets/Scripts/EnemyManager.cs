using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO: Finish raycasting for the enemy AI
 * - Make all of the values negative when the enemy is flipped so all of the rays are actually in the right spot
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
        walls = 1 << LayerMask.NameToLayer("Platforms");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        enemy = Resources.Load<RuntimeAnimatorController>("Animations/Enemy");
        angryEnemy = Resources.Load<RuntimeAnimatorController>("Animations/AngryEnemy");

        sr = GetComponent<SpriteRenderer>();

        moveLeft = randomBoolValue();
        StartCoroutine(walk());
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

    // note to self: might not actually need to use a silly lil method for making our ghosty boye walk
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
        return false;
    }

    /// <summary>
    /// make the lil ghosty boye walk BUT stop if there's a wall ahead so he doesn't scronch into it
    /// </summary>
    /// <param name="flipped">determines whether the ghosty boye is flipped or not</param>
    private IEnumerator walk()
    {
        sr.flipX = moveLeft;
        while(!wallCheck())
        {
            float velocity = 15f;
            if (sr.flipX)
                velocity *= -1;
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            yield return new WaitForSeconds(0.001f);
        }

        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);

        moveLeft = !moveLeft;
        StartCoroutine(walk());
        yield break;
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
