using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Patch up all of the animator controllers so they're not just entry garbage,
    // design them like they're meant to be designed, as a state machine, dummy
    // note: if i just use the animatorcontrollers like they're meant to be used i think i can just get rid of this class
// TODO: Make this a MonoBehaviour because that would be way more convenient
public class MovementAnimation : MonoBehaviour
{
	//public Sprite idleSprite;
    //public Sprite idleEyesSprite;
    RuntimeAnimatorController controller;
    //public RuntimeAnimatorController movement;
    //public RuntimeAnimatorController movementEyes;
    //public Sprite jump;

    Animator animator;
    Animator eyeAnimator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    SpriteRenderer eyeSr;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;
        eyeAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        eyeSr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        animate();
    }

    /// <summary>
    /// Runs all of the animation methods at once.
    /// Call this in an update function or something on the object that needs to be animated.
    /// </summary>
    public void animate()
    {
        animateSprite(rb.velocity);
        //animateEyes();
        flipSprite();
    }

	/// <summary>
    /// Animate a ghost based on their rigidbody velocity.
    /// </summary>
    /// <param name= "movement">
	/// The object's movement; pass in rigidbody velocity
    /// </param>
    private void animateSprite(Vector2 movement)
    {
        // if the ghost's y velocity is actually something, play the jump animation
        if(movement.y != 0)
            //sr.sprite = this.jump;
            updateAnimators("Jumping", true);
        // if the ghost is moving to the left or right, play a walking animation if it's given
        else if(movement.x != 0)
            //animator.runtimeAnimatorController = this.movement;
            updateAnimators("Moving", true);
        // if the ghost isn't moving at all, play an idle animation or just use an idle sprite
        else
            //if(idle != null)
                //animator.runtimeAnimatorController = this.idle;
                updateAnimators(new string[]{"Jumping", "Moving"}, false);
            //else
                //sr.sprite = idleSprite;
    }

    /* 
    /// <summary>
    /// Animate the eyes based on which animation is currently playing.
    /// </summary>
    private void animateEyes()
    {
        if(animator.runtimeAnimatorController == movement)
            eyeAnimator.runtimeAnimatorController = movementEyes;
        else
            eyeSr.sprite = idleEyesSprite;
    }
    */

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
        animator.SetBool(param, value);
        eyeAnimator.SetBool(param, value);
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
