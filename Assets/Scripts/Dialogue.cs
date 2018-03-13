using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private GameObject focusPoint;
    public GameObject FocusPoint
    {
        get { return focusPoint; }
    }



    string printedDialogue;
    public string PrintedDialogue
    {
        get { return printedDialogue; }
        set
        {
            // update the ghost's speech bubble thingy
            dialogueText.text = value;

            printedDialogue = value;
        }
    }

    CameraFollower follower;

    GameObject textObject;
    Text dialogueText;

    private void Awake()
    {
        follower = FindObjectOfType<CameraFollower>();
        
    }

    bool skipDialogue;
    bool endOfLine;
    bool nextLine;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.X))
            skipDialogue = true;

        if (Input.GetKeyDown(KeyCode.Z) && endOfLine)
            nextLine = true;
	}

    PlayerMovement playerMover;
    GhostTalk talker;

    public void startConversation(Message dialogue, GameObject focus, PlayerMovement playerMovement, GhostTalk talk)
    {
        playerMover = playerMovement;
        talker = talk;

        // prevent the player from moving and stop them where they are so they can have a nice polite ghost conversation
        playerMover.enabled = false;
        playerMover.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        // set the focus point
        focusPoint = focus;

        // create the ghost's lil text bubble
        textObject = Instantiate(Resources.Load<GameObject>("Prefabs/WorldText"), focus.transform.position, Quaternion.identity);

        // move the lil text bubble just above both of the ghosts
        textObject.transform.parent = focus.transform;
        textObject.transform.localPosition = new Vector2(0, 9);

        // snatch the text component of the ghost's dialogue bubble so it can be changed
        dialogueText = textObject.GetComponent<Text>();

        // actually start the conversation now that we have a text bubble to show what the ghost is saying
        StartCoroutine(conversationInit(dialogue));
    }

    /// <summary>
    /// The actual conversation part of the conversation; gets a list of lines for a ghost to say and has him say them.
    /// If the player presses the Z key at the end of a line, it will advance to the next one.
    /// If the player presses the X key in the middle of a line, then it will fill the line in instead of doing the slow-fill stuff.
    /// </summary>
    /// <param name="dialogue">
    /// A list of things for the ghost to say.
    /// </param>
    private IEnumerator conversationInit(Message dialogue)
    {
        follower.ghostConversation(focusPoint);

        // run through each of the lines of dialogue for the ghost to say
        for (int index = 0; index < dialogue.messages.Length; index++)
        {
            skipDialogue = false;
            nextLine = false;

            endOfLine = false;

            // convert the current line to a list of chars
            char[] lineLetters = dialogue.messages[index].ToCharArray();
            
            // slowly fill the current line in using these chars
            for (int letter = 0; letter < lineLetters.Length; letter++)
            {
                // if the player presses the x key, fill all the dialogue in and break this loop
                if (skipDialogue)
                {
                    for (int position = letter; position < lineLetters.Length; position++)
                        PrintedDialogue += lineLetters[position];
                    break;
                }

                PrintedDialogue += lineLetters[letter];
                dialogueText.fontSize = dialogue.messageSize[index];

                yield return new WaitForSeconds(dialogue.messagePlayLength[index]);
            }

            // update the status of the dialogue so the player now has a chance to go to the next line
            endOfLine = true;

            // once the line is fully filled in, wait for the player to press the z key to advance to the next line
            while (!nextLine)
                yield return new WaitForSeconds(.1f);

            // go to the next line
            PrintedDialogue = "";
        }

        // once the ghost has said of all of his lines, end the conversation
        endConversation();
    }

    /// <summary>
    /// End the conversation with the ghost.
    /// </summary>
    private void endConversation()
    {
        follower.endGhostConversation(gameObject);
        playerMover.enabled = true;
        talker.talkingToGhost = false;
    }

}

/// <summary>
/// me shitty array methods
/// </summary>
public static class Array
{
    /// <summary>
    /// Fills an array
    /// </summary>
    /// <param name="array">Array to fill.</param>
    /// <param name="value">Value to fill it with.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    public static void fillArray<T>(this T[] array, T value)
    {
        for (int index = 0; index < array.Length; index++)
            array[index] = value;
    }
}

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

    public Message(string[] message)
    {
        messages = message;

        initializeDefaultSize();
        initializeDefaultLength();
    }

    public Message(string[] message, float[] playLength)
    {
        messages = message;

        initializeDefaultSize();
    }

    public Message(string[] message, int[] size)
    {
        messages = message;

        messageSize = size;
        initializeDefaultLength();
    }

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

}

/// <summary>
/// Contains a bunch of preset messages to use in conjunction with the Message class w/ maybe some properties 
/// </summary>
public static class PresetMessages
{
    private static string[] startGhostMessage =
    {
        "hello",                        // 1
        "welcome to heck",              // 2
        "you gotta find your body",     // 3
        "if you wanna leave",           // 4
        "you could always stay though", // 5
        "i could use a pal",            // 6
        "oh well",                      // 7
        "have fun",                     // 8
        "i guess"                       // 9
    };

    // message for the first ghost the player sees to say
    public static Message StartGhostMessage()
    {
        float[] startGhostMessageLength = new float[startGhostMessage.Length];
        startGhostMessageLength.fillArray(Message.DefaultLength);
        startGhostMessageLength[8] = .15f;

        int[] startGhostMessageSize = new int[startGhostMessage.Length];
        startGhostMessageSize.fillArray(Message.DefaultSize);
        startGhostMessageSize[8] = 35;

        return new Message(startGhostMessage, startGhostMessageLength, startGhostMessageSize);
    }

}


