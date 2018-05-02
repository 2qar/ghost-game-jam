using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**VERY SUPER IMPORTANT!!!!
 * TODO: Consider switching from this lame pixel art to some fancy and cozy normal art because it would probably be a whole lot better than the current art style
 *     maybe use Illustrator to create all of the cute ghost art unless you can find a drawing tablet again because otherwise hehe im shit out of luck here bucko
 *     
 * I feel like this dumb ol pixel art style is really crampin on me ya know
 * I'm super limited if I keep using this pixel art style and it's really annoying because there's a lot of things that I want to add that can't really be conveyed in an 8x8 space
 */

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
