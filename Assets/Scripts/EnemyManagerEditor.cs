using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

// FIXME: Valeus get cleared at runtime
    // maybe dont use this 

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    SerializedProperty messageCode;
    SerializedProperty messageOverride;
    SerializedProperty useManualLines;

    public string code;
    public bool manual;
    string[] overrideMessages;
    int lines;

    bool manualOverride;
    bool menuOpened;


    private void OnEnable()
    {
        // dumb property shit
        messageCode = serializedObject.FindProperty("lines");
        messageOverride = serializedObject.FindProperty("lineOverride");
        useManualLines = serializedObject.FindProperty("useManualLines");

        code = messageCode.stringValue;
        manual = useManualLines.boolValue;
        // FIXME
        //overrideMessages = messageOverride;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(messageCode);

        // a toggle switch for manual override; if it's on, show the menu for manual override
        manualOverride = EditorGUILayout.Toggle("Manual Override?", manualOverride);
        if (manualOverride)
            manualOverrideUI();
        else
            useManualLines.boolValue = false;

        if (GUI.changed)
            Undo.RecordObject(target, "");

        serializedObject.ApplyModifiedProperties();
    }

    private void manualOverrideUI()
    {
        useManualLines.boolValue = true;

        // dropdown for the manual override ui
        menuOpened = EditorGUILayout.Foldout(menuOpened, "Override Lines");

        // if the dropdown is clicked,
        if(menuOpened)
        {
            // let the user define how many elements should be in the override 
            lines = EditorGUILayout.DelayedIntField("Lines", lines);
            messageOverride.arraySize = lines;

            // show array elements
            for (int index = 0; index < messageOverride.arraySize; index++)
            {
                // show the current line with the correct label
                GUIContent label = new GUIContent("Line " + (index + 1));
                EditorGUILayout.PropertyField(messageOverride.GetArrayElementAtIndex(index), label);
            }
        }
            
    }

}