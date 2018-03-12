using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTalk : MonoBehaviour
{
    SpriteRenderer sr;
    PlayerMovement playerMovement;

    [HideInInspector]
    public bool talkingToGhost;

	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        talkToGhostInput();
    }

    /// <summary>
    /// Talks to a ghost if the player is within talking distance.
    /// </summary>
    void talkToGhostInput()
    {
        Collider2D ghost = ghostToTalkTo("Player");

        bool canTalkToGhost = false;

        // if the ghost collider actually exists and actually is a ghost, the player can talk to a ghost
        if (ghost != null)
            if (ghost.ToString().Contains("FriendlyEnemy"))
                canTalkToGhost = true;

        // if the player can talk to a ghost and they press the talk key, guess what happens
        if (canTalkToGhost && Input.GetKeyDown(KeyCode.Z) && !talkingToGhost)
        {
            talkToGhost(ghost.gameObject);
        }
    }

    /// <summary>
    /// Have a conversation with a ghost
    /// </summary>
    void talkToGhost(GameObject ghost)
    {
        talkingToGhost = true;

        // make the ghost face the player for the conversation like a polite boy
        SpriteRenderer ghostSr = ghost.GetComponent<SpriteRenderer>();
        if (sr.flipX)
            ghostSr.flipX = false;
        else
            ghostSr.flipX = true;

        GameObject focus = createFocusPoint(ghost, transform);

        // attach the dialogue management script to the focus point to handle the actual conversation stuff
        Dialogue dialogue = focus.AddComponent<Dialogue>();
        dialogue.startConversation(Dialogue.StartDialogue, focus, playerMovement, this);
    }

    /// <summary>
    /// Create a focus point for the camera between the ghost to talk to and the player.
    /// </summary>
    /// <param name="ghost">
    /// Ghost to have a nice little conversation with.
    /// </param>
    /// <param name="player">
    /// The player.
    /// </param>
    /// <returns>
    /// The focus point created between the two.
    /// </returns>
    private GameObject createFocusPoint(GameObject ghost, Transform player)
    {
        // get the distance between the player and the ghost
        float distance = Vector2.Distance(transform.position, ghost.transform.position);

        // get a point that's in the middle of the ghost and the player
        Vector2 focusPoint;
        if (ghost.transform.position.x > transform.position.x)
            focusPoint = new Vector2(transform.position.x + (distance / 2), transform.position.y);
        else
            focusPoint = new Vector2(ghost.transform.position.x + (distance / 2), transform.position.y);

        // create a focus point gameobject at the point between the two
        GameObject focus = new GameObject("FocusPoint");
        focus.transform.position = focusPoint;

        return focus;
    }

    /// <summary>
    /// Checks if the player is within talking distance of a ghost.
    /// </summary>
    /// <param name="layerMask">
    /// Layermask to ignore, usually the player
    /// </param>
    /// <returns>
    /// The collider that the player's wacky ray hits
    /// </returns>
    Collider2D ghostToTalkTo(string layerMask)
    {
        // set up a layermask so that the raycast can hit every layer except for the player
        LayerMask playerMask = 1 << LayerMask.NameToLayer(layerMask);
        playerMask = ~playerMask;

        // direction to fire the ray in based on the direction the player is facing
        Vector2 direction;
        if (sr.flipX)
            direction = Vector2.left;
        else
            direction = Vector2.right;

        // the collider that the player's ray hits
        return Physics2D.Raycast(transform.position, direction, 13, playerMask).collider;
    }

}
