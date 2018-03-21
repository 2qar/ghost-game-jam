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

    /*
    // line code that grabs a certain message from presetmessages
    public string lines;

    public int overrideLength;

    public Message message;
    private Message generatedMessage;

    [SerializeField]
    public string[] overrideLines = new string[3];

    [SerializeField]
    public bool useManualLines;
    public bool UseManualLines
    {
        get { return useManualLines; }
        set
        {
            if (value)
                message = new Message(overrideLines);
            else
                message = generatedMessage;

            useManualLines = value;
        }
    }

    // only exists because i don't know how to save stuff that isn't a serializedproperty
    public bool menuOpened;
    */
	// Use this for initialization
	void Start ()
    {
        // TODO: Rewrite this so that it picks a random message every time the player talks to this ghost
            // Make the dialogue thing check if the message is null; if it's null, pick a random message
        // if no message is given, generate a random message
        if(ghostMessage == null)
        {
            ghostMessage = new GhostMessage();
            ghostMessage.AddRandomMessage();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}