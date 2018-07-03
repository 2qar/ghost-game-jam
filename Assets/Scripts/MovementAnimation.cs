using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update a parameters on an object's animator controller so that movement is animated based on given input.
/// </summary>
[RequireComponent(typeof(Animator))]
public class MovementAnimation : MonoBehaviour
{
    RuntimeAnimatorController controller;

    Animator animator;
    Animator eyeAnimator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    SpriteRenderer eyeSr;

    public bool grounded;
    public bool pressingMovementKey;

    void Start()
    {
        GameObject eyes = transform.GetChild(0).gameObject;

        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;
        eyeAnimator = eyes.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        eyeSr = eyes.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        try { animate(); }
        catch{}
    }

    /// <summary>
    /// Runs all of the animation methods at once.
    /// Call this in an update function or something on the object that needs to be animated.
    /// </summary>
    public void animate()
    {
        animateSprite();
        //animateEyes();
        flipSprite();
    }

	/// <summary>
    /// Set flags for the animator to play the correct animations based on whether or not the ghost to animate is moving or not and stuff like that
    /// </summary>
    /// <param name= "movement">
	/// The object's movement; pass in rigidbody velocity
    /// </param>
    private void animateSprite()
    {
        updateAnimators("Jumping", !grounded);

        updateAnimators("Moving", pressingMovementKey);
    }

    /// <summary>
    /// Flip the sprite based on movement.
    /// </summary>
    private void flipSprite()
    {
        if(rb.velocity.x < 0)
        {
            sr.flipX = true;
            eyeSr.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            sr.flipX = false;
            eyeSr.flipX = false;
        }
    }

    /// <summary>
    /// Update a parameter on both of the animators at the same time.
    /// </summary>
    private void updateAnimators(string param, bool value)
    {
        try { animator.SetBool(param, value); }
        catch {}
        try { eyeAnimator.SetBool(param, value); }
        catch {}
    }

    /// <summary>
    /// Update multiple parameters on both of the animators at the same time.
    /// </summary>
    private void updateAnimators(string[] param, bool value)
    {
        foreach(string parameter in param)
            updateAnimators(parameter, value);
    }

}
