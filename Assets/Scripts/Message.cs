using UnityEngine;

// TODO: Remove Message in favor of RealMessage because RealMessage makes more sense

/// <summary>
/// Create a message with properties like length to play a message and the size for each message.
/// </summary>
/*
[System.Serializable]
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
        messages = new string[0];

        initializeDefaultSize();
        initializeDefaultLength();
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
*/

/// <summary>
/// A message to be said by a ghost; has its own text, play length, and text size properties.
/// </summary>
[System.Serializable]
public class RealMessage
{
    public const float DefaultLength = .1f;
    public const int DefaultSize = 50;

    public string message;
    public float playLength;
    public int size;

    public RealMessage()
    {
        playLength = DefaultLength;
        size = size = DefaultSize;
    }

}