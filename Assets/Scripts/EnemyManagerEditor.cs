using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
[CanEditMultipleObjects]
public class EnemyManagerEditor : Editor
{
    EnemyManager enemy;

    SerializedProperty messageCode;

    SerializedProperty messageOverride;
    bool manualOverride;
    bool menuOpened;
    int lines;

    SerializedProperty useManualLines;

    private void OnEnable()
    {
        messageCode = serializedObject.FindProperty("lines");
        messageOverride = serializedObject.FindProperty("lineOverride");

        useManualLines = serializedObject.FindProperty();
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
            useManualLines = false;
            
        serializedObject.ApplyModifiedProperties();
    }

    private void manualOverrideUI()
    {
        useManualLines = true;

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