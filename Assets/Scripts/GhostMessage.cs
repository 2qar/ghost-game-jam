using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// NOTE: ANY TIME YOU CHANGE THIS CLASS, YOU HAVE TO REPLACE ALL OF THE OBJECTS IN THE MESSAGES FOLDER
// HERE'S A TIP: NEVER CHANGE THIS FUCKING FILE YOU BUFFOON

[CreateAssetMenu(fileName = "GhostMessage", menuName = "Ghost Message")]
[System.Serializable]
public class GhostMessage : ScriptableObject
{
    public List<RealMessage> messages = new List<RealMessage>();

    public void AddRandomMessage()
    {
        // pick a random message
        GhostMessage randomPicker = Resources.Load("GhostMessages/RandomMessages") as GhostMessage;
        int randomMessage = Random.Range(0, randomPicker.messages.Count);

        // add this random message to the list of messages
        messages.Add(randomPicker.messages[randomMessage]);
    }
}