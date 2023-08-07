using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Conditions))]
public class ConditionsDrawerUIE : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect nameRect = new Rect(position.x, position.y, 100, 20);
        Rect conditionRect = new Rect(position.x + 110, position.y, 100, 20);

        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("conditionName"), GUIContent.none);
        EditorGUI.PropertyField(conditionRect, property.FindPropertyRelative("condition"), GUIContent.none);

        Rect desiredLabelRect = new Rect(position.x + 220, position.y, 500, 20);
        EditorGUI.LabelField(desiredLabelRect, "Desired");

        Conditions.Condition setCondition = (Conditions.Condition)property.FindPropertyRelative("condition").enumValueIndex;

        Rect desiredPropertyRect = new Rect(position.x + 330, position.y, 20, 20);

        switch (setCondition) 
        {

            case Conditions.Condition.Toggle:
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("toggle"), GUIContent.none);
                break;

            case Conditions.Condition.Count:
                desiredPropertyRect.width += 10;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("count"), GUIContent.none);
                break;

            case Conditions.Condition.FloatCount:
                desiredPropertyRect.width += 30;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("floatCount"), GUIContent.none);
                break;

            case Conditions.Condition.Word:
                desiredPropertyRect.width += 80;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("word"), GUIContent.none);
                break;

        }

        Rect currentLabelRect = new Rect(position.x + 220, position.y + 25, 500, 20);
        EditorGUI.LabelField(currentLabelRect, "Current");

        Rect currentPropertyRect = new Rect(position.x + 330, position.y + 25, 20, 20);

        switch (setCondition)
        {

            case Conditions.Condition.Toggle:               
                EditorGUI.PropertyField(currentPropertyRect, property.FindPropertyRelative("currentToggle"), GUIContent.none);
                break;

            case Conditions.Condition.Count:
                currentPropertyRect.width += 10;
                EditorGUI.PropertyField(currentPropertyRect, property.FindPropertyRelative("currentCount"), GUIContent.none);
                break;

            case Conditions.Condition.FloatCount:
                currentPropertyRect.width += 30;
                EditorGUI.PropertyField(currentPropertyRect, property.FindPropertyRelative("currentFloatCount"), GUIContent.none);
                break;

            case Conditions.Condition.Word:
                currentPropertyRect.width += 80;
                EditorGUI.PropertyField(currentPropertyRect, property.FindPropertyRelative("currentWord"), GUIContent.none);
                break;

        }

        Rect completedLabelRect = new Rect(position.x + 220, position.y + 50, 100, 20);
        EditorGUI.LabelField(completedLabelRect, "Completed");

        Rect completedRect = new Rect(position.x + 330, position.y + 50, 20, 20);
        EditorGUI.PropertyField(completedRect, property.FindPropertyRelative("completed"), GUIContent.none);

        Rect bottomLineRect = new Rect(25, position.y + 75, 750, 20);
        EditorGUI.LabelField(bottomLineRect, "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------2");

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 100;
    }
}
