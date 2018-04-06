using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Make this static 
// TODO: Tidy up the start conversation method by removing some references not needed ex. ghosttalk, playermove
// TODO: Maybe make the ghost also use the talk prompt for dialogue instead of instantiating a new text box

/// <summary>
/// sets up a nice conversation with a friendly ghosty boye
/// </summary>
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
    Shadow textShadow;

    private void Awake()
    {
        follower = FindObjectOfType<CameraFollower>();
    }

    // bunch of conversation stuff
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

    /// <summary>
    /// Starts a conversation with a ghost.
    /// </summary>
    /// <param name="dialogue">message for the ghost to say</param>
    /// <param name="focus">camera focus point</param>
    public void startConversation(GhostMessage dialogue, GameObject focus)
    {
        playerMover = PlayerMovement.instance;
        talker = GhostTalk.instance;

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
        textShadow = textObject.GetComponent<Shadow>();

        // make the ghost be a polite boye and stop walking
        talker.ghost.GetComponent<EnemyManager>().StopAllCoroutines();
        talker.ghost.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        // actually start the conversation now that we have a text bubble to show what the ghost is saying
        StartCoroutine(conversationInit(dialogue));
    }

    /// <summary>
    /// The actual conversation part of the conversation; gets a list of lines for a ghost to say and has him say them.
    /// Z key advances to next line
    /// X key mid-line fills the line in instead of waiting slowly to fill it in
    /// </summary>
    /// <param name="dialogue">
    /// A message for the ghost to say.
    /// </param>
    private IEnumerator conversationInit(GhostMessage dialogue)
    {
        follower.ghostConversation(focusPoint);

        // if the ghost being talked to wasn't assigned a message, pick a random one
        if(dialogue == null)
        {
            dialogue = new GhostMessage();
            dialogue.AddRandomMessage();
        }

        // run through each of the lines of dialogue for the ghost to say
        for (int index = 0; index < dialogue.messages.Count; index++)
        {
            skipDialogue = false;
            nextLine = false;

            endOfLine = false;

            // convert the current line to a list of chars
            char[] lineLetters = dialogue.messages[index].message.ToCharArray();
            textShadow.effectDistance = new Vector2(0, dialogue.messages[index].size / -7.69f);

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
                dialogueText.fontSize = dialogue.messages[index].size;

                yield return new WaitForSeconds(dialogue.messages[index].playLength);
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
        talker.ghost.GetComponent<EnemyManager>().StartCoroutine("walk");
        follower.endGhostConversation(gameObject);
        playerMover.enabled = true;
        talker.talkingToGhost = false;
    }

}