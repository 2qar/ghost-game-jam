using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Create a message with properties like length to play a message and the size for each message.
/// </summary>
public class Message
{
    public string[] messages;

    public float[] messagePlayLength;
    public static float DefaultLength = .1f;

    public int[] messageSize;
    public static int DefaultSize = 50;

    // create an empty message
    public Message()
    {
        new Message(new string[0], new float[0], new int[0]);
    }

    // create a message w/ default size and length
    public Message(string[] message)
    {
        messages = message;

        initializeDefaultSize();
        initializeDefaultLength();
    }

    // create a message with a given play length
    public Message(string[] message, float[] playLength)
    {
        messages = message;

        initializeDefaultSize();
    }

    // create a message with given sizes
    public Message(string[] message, int[] size)
    {
        messages = message;

        messageSize = size;
        initializeDefaultLength();
    }

    // create a message w/ a given length and size
    public Message(string[] message, float[] playLength, int[] size)
    {
        messages = message;

        messagePlayLength = playLength;
        messageSize = size;
    }

    private void initializeDefaultLength()
    {
        messagePlayLength = new float[messages.Length];
        Array.fillArray(messagePlayLength, DefaultLength);
    }

    private void initializeDefaultSize()
    {
        messageSize = new int[messages.Length];
        Array.fillArray(messageSize, DefaultSize);
    }

    public void printMessage()
    {
        foreach (string message in messages)
            Debug.Log(message);
    }

}

public class RealMessage
{
    public string message;
    public float playLength;
    public int size;

    public RealMessage()
    {
        playLength = Message.DefaultLength;
        size = Message.DefaultSize;
    }

}

[CreateAssetMenu(fileName = "GhostMessage", menuName = "Ghost Message")]
public class GhostMessage : ScriptableObject
{
    public List<RealMessage> messages = new List<RealMessage>();
}

// FIXME: Fix the list not decreasing in size ever

[CustomEditor(typeof(GhostMessage))]
public class GhostMessageEditor : Editor
{
    GhostMessage message;

    int messageListSize;

    private void Awake()
    {
        message = (GhostMessage)target;
    }

    public override void OnInspectorGUI()
    {
        displayMessages();
    }

    private void displayMessages()
    {
        // pick how many messages to have
        messageListSize = EditorGUILayout.DelayedIntField("Messages", messageListSize);
        int messageCount = message.messages.Count;
        if (messageListSize < messageCount)
            messageListSize = messageCount;
        while (message.messages.Count < messageListSize)
            message.messages.Add(new RealMessage());

        int messageBoxHeight = 60;
        EditorGUILayout.Space();

        Debug.Log(message.messages.Count);

        // print all of the message elements
        for (int index = 0; index < message.messages.Count; index++)
        {
            /*
            Rect messageGroup = EditorGUILayout.BeginVertical();
            messageGroup.y = messageBoxHeight * (index + 1);

            Rect messageRect = createMessageBox(messageGroup);

            message.messages[index] = EditorGUI.DelayedTextField(messageRect, "Message " + (index + 1), message.messages[index]);

            Rect playLengthRect = messageRect;
            playLengthRect.y += 15;

            message.messagePlayLength[index] = EditorGUI.DelayedFloatField(playLengthRect, message.messagePlayLength[index]);


            EditorGUILayout.EndVertical();
            */

            message.messages[index].message = EditorGUILayout.DelayedTextField("Message " + (index + 1), message.messages[index].message);
            message.messages[index].playLength = EditorGUILayout.DelayedFloatField("Play Length", message.messages[index].playLength);
            message.messages[index].size = EditorGUILayout.DelayedIntField("Size", message.messages[index].size);

            if(GUILayout.Button("Delete Message"))
            {
                if(!(index >= message.messages.Count))
                    message.messages.RemoveAt(index);
            }

            message.messages.TrimExcess();
            EditorGUILayout.Space();
        }

        //EditorGUILayout.EndVertical();

    }

    private Rect createMessageBox(Rect messageGroup)
    {
        Rect messageRect = messageGroup;
        messageRect.x += 20;
        messageRect.y += 15;
        messageRect.width -= 20;
        messageRect.height = 16;

        return messageRect;
    }

}
