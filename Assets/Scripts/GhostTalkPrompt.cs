using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Add this to the ghost gameobject instead of putting it on a seperate object

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

}
