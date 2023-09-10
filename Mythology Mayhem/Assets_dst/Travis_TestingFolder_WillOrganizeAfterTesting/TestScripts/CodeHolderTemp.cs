/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Conditions)), CanEditMultipleObjects]
public class ConditionsEditor : Editor
{
    public SerializedProperty
        conditionProperty,
        toggleProperty,
        countProperty,
        floatcountProperty,
        wordProperty;
    

    private void OnEnable()
    {
        
        conditionProperty = serializedObject.FindProperty("condition");
        toggleProperty = serializedObject.FindProperty("toggle");
        countProperty = serializedObject.FindProperty("count");
        floatcountProperty = serializedObject.FindProperty("floatCount");
        wordProperty = serializedObject.FindProperty("word");
        
    }

    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();

        EditorGUILayout.PropertyField(conditionProperty);

        Conditions.Condition cond = (Conditions.Condition)conditionProperty.enumValueIndex;

        //GUILayoutOption[] optionHolder = new GUILayoutOption[0];

        switch (cond) 
        {

            case Conditions.Condition.Toggle:
                EditorGUILayout.PropertyField(toggleProperty, new GUIContent("Toggle"));
                serializedObject.FindProperty("count").intValue = 0;
                serializedObject.FindProperty("floatCount").floatValue = 0;
                serializedObject.FindProperty("word").stringValue = "";
                break;
            case Conditions.Condition.Count:
                EditorGUILayout.PropertyField(countProperty, new GUIContent("Count"));
                serializedObject.FindProperty("toggle").boolValue = false;
                serializedObject.FindProperty("floatCount").floatValue = 0;
                serializedObject.FindProperty("word").stringValue = "";
                break;
            case Conditions.Condition.FloatCount:
                EditorGUILayout.PropertyField(floatcountProperty, new GUIContent("FloatCount"));
                serializedObject.FindProperty("toggle").boolValue = false;
                serializedObject.FindProperty("count").intValue = 0;
                serializedObject.FindProperty("word").stringValue = "";
                break;
            case Conditions.Condition.Word:
                EditorGUILayout.PropertyField(wordProperty, new GUIContent("Word"));
                serializedObject.FindProperty("toggle").boolValue = false;
                serializedObject.FindProperty("count").intValue = 0;
                serializedObject.FindProperty("floatCount").floatValue = 0;
                break;
        }


        serializedObject.ApplyModifiedProperties();
        
    }
}
*/
