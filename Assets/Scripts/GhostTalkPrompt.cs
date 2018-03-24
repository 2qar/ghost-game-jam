﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Maybe add some pink to the text prompts

/// <summary>
/// Show and hide the ghost's talk prompt when the player gets close enough.
/// </summary>
[RequireComponent(typeof(Text))]
public class GhostTalkPrompt : MonoBehaviour 
{
    private Text talkPrompt;

    private bool showTalkPrompt;
    public bool ShowTalkPrompt
    {
        get { return showTalkPrompt; }
        set
        {
            if (value)
                talkPrompt.enabled = true;
            else
                talkPrompt.enabled = false;
            showTalkPrompt = value;
        }
    }

    private void Awake()
    {
        // get the text component and hide it just to make sure 
        talkPrompt = GetComponent<Text>();
        talkPrompt.enabled = false;

        // change the layer that the trigger zone will be on so that the player's jump raycast stuff doesn't break
        gameObject.layer = 9;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            ShowTalkPrompt = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            ShowTalkPrompt = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // if the player is in the collider but already talking to a ghost, hide the prompt
        if (collision.gameObject.tag == "Player" && GhostTalk.instance.talkingToGhost)
            ShowTalkPrompt = false;
        // if the player is in the collider but they're not looking at the ghost, hide the prompt
        else
            ShowTalkPrompt = true;
    }
    */

}
