using UnityEditor;
using UnityEngine;

namespace LeverSystem
{
    [CustomEditor(typeof(LeverItem))]
    public class TwoDLeverItemEditor : Editor
{
        SerializedProperty _objectType;
        SerializedProperty leverNumber;
        SerializedProperty animationName;
        SerializedProperty _leverSystemController;

        private void OnEnable()
        {
            _objectType = serializedObject.FindProperty(nameof(_objectType));
            leverNumber = serializedObject.FindProperty(nameof(leverNumber));
            animationName = serializedObject.FindProperty(nameof(animationName));
            _leverSystemController = serializedObject.FindProperty(nameof(_leverSystemController));
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((LeverItem)target), typeof(LeverItem), false);
            GUI.enabled = true;

            EditorGUILayout.Space(5);

            LeverItem _leverItem = (LeverItem)target;

            EditorGUILayout.PropertyField(_objectType);

            EditorGUILayout.Space(5);

            if (_leverItem._objectType == LeverItem.ObjectType.Lever)
            {
                EditorGUILayout.PropertyField(leverNumber);
                EditorGUILayout.Space(5);
            }

            EditorGUILayout.PropertyField(animationName);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Controller Reference", EditorStyles.toolbarTextField);
            EditorGUILayout.PropertyField(_leverSystemController);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
