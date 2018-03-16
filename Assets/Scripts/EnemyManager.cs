using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: Find a more efficient way to update the ghost's message based on whether manual override is on that doesn't involve updating the message every frame

[ExecuteInEditMode]
[Serializable]
public class EnemyManager : MonoBehaviour
{
    public string lines;

    public Message message;
    private Message generatedMessage;

    public string[] lineOverride = new string[3];

    [HideInInspector]
    [SerializeField]
    private bool useManualLines;
    /*
    public bool UseManualLines
    {
        get { return useManualLines; }
        set
        {
            if (value)
            {
                message = new Message(lineOverride);
                Debug.Log("set true");
            }
            else
            {
                message = generatedMessage;
                Debug.Log("set false");
            }
            
            useManualLines = value;
        }
    }
    */

	// Use this for initialization
	void Start ()
    {
        generatedMessage = PresetMessages.GenerateMessage(lines);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (useManualLines)
            message = new Message(lineOverride);
        else
            message = generatedMessage;
    }

}