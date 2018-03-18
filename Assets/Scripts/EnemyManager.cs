using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a bunch of enemy stuff, like what lines to say, and whether to use manually written lines or not.
/// </summary>
[System.Serializable]
[ExecuteInEditMode]
public class EnemyManager : MonoBehaviour
{
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

	// Use this for initialization
	void Start ()
    {
        // generate a message for the ghost and give it to the ghost by default
        generatedMessage = PresetMessages.GenerateMessage(lines);
        //message = generatedMessage;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

}