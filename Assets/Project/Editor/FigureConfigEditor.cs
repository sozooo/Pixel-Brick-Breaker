using UnityEditor;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling.Editor
{
    [CustomEditor(typeof(FigureConfig))]
    public class FigureConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            FigureConfig config = (FigureConfig)target;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Flip X"))
            {
                Undo.RecordObject(config, "Flip Figure X");
                config.FlipX();
                EditorUtility.SetDirty(config);
            }

            if (GUILayout.Button("Flip Y"))
            {
                Undo.RecordObject(config, "Flip Figure Y");
                config.FlipY();
                EditorUtility.SetDirty(config);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
