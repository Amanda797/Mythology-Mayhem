using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Conditional))]
public class ConditionalDrawerUIE : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect nameRect = new Rect(position.x, position.y, 100, position.height);
        Rect conditionRect = new Rect(position.x + 110, position.y, 100, position.height);

        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("conditionName"), GUIContent.none);
        EditorGUI.PropertyField(conditionRect, property.FindPropertyRelative("type"), GUIContent.none);

        Conditions.Condition setCondition = (Conditions.Condition)property.FindPropertyRelative("type").enumValueIndex;

        Rect desiredLabelRect = new Rect(position.x + 220, position.y, 500, position.height);
        Rect desiredPropertyRect = new Rect(position.x + 250, position.y, 20, position.height);

        switch (setCondition)
        {

            case Conditions.Condition.Toggle:
                EditorGUI.LabelField(desiredLabelRect, "Set");
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("setToggle"), GUIContent.none);
                break;

            case Conditions.Condition.Count:
                EditorGUI.LabelField(desiredLabelRect, "Add");
                desiredPropertyRect.width += 10;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("addCount"), GUIContent.none);
                break;

            case Conditions.Condition.FloatCount:
                EditorGUI.LabelField(desiredLabelRect, "Add");
                desiredPropertyRect.width += 30;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("addFloatCount"), GUIContent.none);
                break;

            case Conditions.Condition.Word:
                EditorGUI.LabelField(desiredLabelRect, "Set");
                desiredPropertyRect.width += 80;
                EditorGUI.PropertyField(desiredPropertyRect, property.FindPropertyRelative("setWord"), GUIContent.none);
                break;

        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();

    }

    /*
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 100;
    }
    */
}
