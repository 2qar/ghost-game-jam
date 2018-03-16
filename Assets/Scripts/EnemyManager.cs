using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Find a more efficient way to update the ghost's message based on whether manual override is on that doesn't involve updating the message every frame

[System.Serializable]
[ExecuteInEditMode]
public class EnemyManager : MonoBehaviour
{
    // line code that grabs a certain message from presetmessages
    public string lines;

    public Message message;
    private Message generatedMessage;

    [SerializeField]
    private bool useManualLines;

    public string[] lineOverride = new string[3];

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