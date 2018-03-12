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

    // dialogue of the starting ghost
    public static string[] StartDialogue
    {
        get
        {
            return new string[]
            {
                "hello",
                "welcome to heck",
                "you gotta find your body",
                "if you wanna leave",
                "have fun"
            };
        }
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

    public void startConversation(string[] dialogue, GameObject focus, PlayerMovement playerMovement, GhostTalk talk)
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
    private IEnumerator conversationInit(string[] dialogue)
    {
        follower.ghostConversation(focusPoint);
        
        // run through each of the lines of dialogue for the ghost to say
        for (int index = 0; index < dialogue.Length; index++)
        {
            skipDialogue = false;
            nextLine = false;

            endOfLine = false;

            // convert the current line to a list of chars
            char[] lineLetters = dialogue[index].ToCharArray();
            
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
                yield return new WaitForSeconds(.1f);
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