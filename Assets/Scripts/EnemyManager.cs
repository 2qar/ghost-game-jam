using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a bunch of enemy stuff, like what lines to say, and whether to use manually written lines or not.
/// </summary>
//[ExecuteInEditMode]
[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    public GhostMessage ghostMessage;

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

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        enemy = Resources.Load<RuntimeAnimatorController>("Animations/Enemy");
        angryEnemy = Resources.Load<RuntimeAnimatorController>("Animations/AngryEnemy");

        moveLeft = randomBoolValue();

        StartCoroutine(move());
	}
	
	// Update is called once per frame
	void Update ()
    {
        //rb.velocity = movement;
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

}
