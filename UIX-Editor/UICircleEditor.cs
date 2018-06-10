using UnityEditor;

namespace UIX.Editor {
    [CustomEditor(typeof(UICircle))]
    public class UICircleEditor : UnityEditor.Editor {
        private UICircle circle;

        private void OnEnable() {
            circle = (UICircle) target;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            circle.TotalVertices = (ushort) EditorGUILayout.IntField("Total Steps", circle.TotalVertices);
        }
    }
}