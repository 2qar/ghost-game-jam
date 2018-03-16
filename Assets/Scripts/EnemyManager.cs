using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: Find a way to have the manual override property show in the inspector

[ExecuteInEditMode]
[Serializable]
public class EnemyManager : MonoBehaviour
{
    public string lines;
    [HideInInspector]
    public Message message;
    private Message generatedMessage;

    public string[] lineOverride = new string[3];

    [SerializeField]
    private bool useManualLines;
    [SerializeField]
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

	// Use this for initialization
	void Start ()
    {
        generatedMessage = PresetMessages.GenerateMessage(lines);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

}