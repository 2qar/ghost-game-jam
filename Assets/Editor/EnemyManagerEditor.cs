using UnityEngine;
using System.Collections;
using UnityEditor;

// FIXME: Values in the inspector aren't used in the built version of the game
    // that's because this script isn't running in the built version of the game you idiot
    // this probably can't be fixed, but making messages a serializable object is a nice way of getting around this even if it is kinda messy
// TODO: Instead of hiding the message override and soon the other message properties when manual override is off, make them locked

// TODO: Rewrite without serialized properties if possible

/// <summary>
/// Custom inspector for the EnemyManager class.
/// </summary>
/*
[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    EnemyManager enemy;

    SerializedProperty lineCode;
    SerializedProperty manualOverride;
    SerializedProperty messageOverride;
    SerializedProperty messages;
    SerializedProperty menuOpened;

    private void Awake()
    {
        enemy = (EnemyManager)target;
    }

    private void OnEnable()
    {
        // get all of the stupid dumb properties off of the enemy
        lineCode = serializedObject.FindProperty("lines");
        manualOverride = serializedObject.FindProperty("useManualLines");
        messageOverride = serializedObject.FindProperty("overrideLines");
        messages = serializedObject.FindProperty("overrideLength");
        menuOpened = serializedObject.FindProperty("menuOpened");
        enemy.UseManualLines = manualOverride.boolValue;
    }

    public override void OnInspectorGUI()
    {
        // show the reference to the enemy script
        //EditorGUILayout.ObjectField("Script", enemy, typeof(EnemyManager));

        serializedObject.Update();

        GUIContent codeLabel = new GUIContent("Preset Message Code");
        EditorGUILayout.DelayedTextField(lineCode, codeLabel);

        // a toggle switch for manual override; if it's on, show the menu for manual override
        manualOverride.boolValue = EditorGUILayout.Toggle("Manual Override?", manualOverride.boolValue);
        if (enemy.UseManualLines)
            manualOverrideUI();
        
        enemy.UseManualLines = manualOverride.boolValue;

        // I might not need this??? i dont really know anymore
        //if (GUI.changed)
            //EditorUtility.SetDirty((EnemyManager)target);

        serializedObject.ApplyModifiedProperties();
    }

    private void manualOverrideUI()
    {
        // dropdown for the manual override ui
        menuOpened.boolValue = EditorGUILayout.Foldout(menuOpened.boolValue, "Override Lines");

        // if the dropdown is clicked,
        if (menuOpened.boolValue)
        {
            // let the user define how many elements should be in the override 
            //lines = EditorGUILayout.DelayedIntField("Lines", lines);
            GUIContent lineLabel = new GUIContent("Lines");
            EditorGUILayout.DelayedIntField(messages, lineLabel);
            messageOverride.arraySize = messages.intValue;

            // show array elements
            for(int index = 0; index < messageOverride.arraySize; index++)
            {
                GUIContent label = new GUIContent("Line " + (index + 1));
                EditorGUILayout.DelayedTextField(messageOverride.GetArrayElementAtIndex(index), label);
            }
        }

    }

}
*/
/*
[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    EnemyManager enemy;

    private void Awake()
    {
        enemy = (EnemyManager)target;
    }

    public override void OnInspectorGUI()
    {
        enemy.ghostMessage = EditorGUILayout.ObjectField("Message", enemy.ghostMessage, typeof(GhostMessage)) as GhostMessage;
    }
}
*/