using UnityEngine;
using UnityEditor;

// FIXME: Fix the list not decreasing in size ever
// FIXME: The delete message button works, but only after clicking off of message
// FIXME: For some reason the inspector randomly doesn't draw anything

// Deleting messages only works when count is 0
[CustomEditor(typeof(GhostMessage))]
[System.Serializable]
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
        DrawDefaultInspector();
        //displayMessages();
    }

    private void displayMessages()
    {
        // pick how many messages to have
        messageListSize = EditorGUILayout.DelayedIntField("Messages", messageListSize);
        int messageCount = message.messages.Count;

        //if (messageListSize < messageCount)
          //  messageListSize = messageCount;
        //while (message.messages.Count < messageListSize)
            //message.messages.Add(new RealMessage());

        message.messages.Capacity = messageCount;
        while (message.messages.Count < messageListSize)
            message.messages.Add(new RealMessage());

        //int messageBoxHeight = 60;
        EditorGUILayout.Space();

        Debug.Log("Message Count : " + message.messages.Count);

        // print all of the message elements
        for (int index = 0; index < message.messages.Capacity; index++)
        {
            /*#region comment
            Rect messageGroup = EditorGUILayout.BeginVertical();
            messageGroup.y = messageBoxHeight * (index + 1);

            Rect messageRect = createMessageBox(messageGroup);

            message.messages[index] = EditorGUI.DelayedTextField(messageRect, "Message " + (index + 1), message.messages[index]);

            Rect playLengthRect = messageRect;
            playLengthRect.y += 15;

            message.messagePlayLength[index] = EditorGUI.DelayedFloatField(playLengthRect, message.messagePlayLength[index]);


            EditorGUILayout.EndVertical();
            #endregion*/

            message.messages[index].message = EditorGUILayout.DelayedTextField("Message " + (index + 1), message.messages[index].message);
            message.messages[index].playLength = EditorGUILayout.DelayedFloatField("Play Length", message.messages[index].playLength);
            message.messages[index].size = EditorGUILayout.DelayedIntField("Size", message.messages[index].size);

            if (GUILayout.Button("Delete Message"))
            {
                if (!(index >= message.messages.Count))
                {
                    Debug.Log("ayy lmao");
                    message.messages.RemoveAt(index);
                }
                else
                    Debug.LogError("index too high : " + index);
                message.messages.TrimExcess();
                //message.messages.Capacity--;
            }

            message.messages.TrimExcess();
            EditorGUILayout.Space();
        }

        //message.messages.TrimExcess();
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
