using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials {
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour {
        public static Color ChangedPrefabColor = new Color(0.7817802f, 0.8f, 0);
        public static Color BackgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f);
        public static Color PrefabTextColor = new Color(0.3843138f, 0.4980392f, 0.6666667f);
        public static Color MissingPrefabColor = new Color(1, 0, 0);
        public static Color HoverOverBackgroundColor = new Color(0.2666667f, 0.2666667f, 0.2666667f);
        static Vector2 _offset = new Vector2(18, 0);

        static CustomHierarchy() {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGui;
        }

        static void HandleHierarchyWindowItemOnGui(int instanceID, Rect selectionRect) {
            var textColor = PrefabTextColor;
            var backgroundColor = BackgroundColor;
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);


            if (selectionRect.Contains(Event.current.mousePosition) && !Selection.instanceIDs.Contains(instanceID) && obj != null) {
                EditorGUI.DrawRect(selectionRect, HoverOverBackgroundColor);
                DrawNewLabel(selectionRect, obj, Color.white);
                DrawGameObjectIcon(selectionRect, content);
                return;
            }

            if (obj == null || !PrefabUtility.IsAnyPrefabInstanceRoot(obj as GameObject)) return;


            if (PrefabUtility.HasPrefabInstanceAnyOverrides(obj as GameObject, false)) {
                textColor = ChangedPrefabColor;
            }
            
            var prefabType = PrefabUtility.GetPrefabAssetType(obj);
            if (prefabType == PrefabAssetType.MissingAsset) {
                textColor = MissingPrefabColor;
            }
            
            if (prefabType == PrefabAssetType.Regular || prefabType == PrefabAssetType.Variant || prefabType == PrefabAssetType.Model || prefabType == PrefabAssetType.MissingAsset) {
                if (Selection.instanceIDs.Contains(instanceID)) {
                    textColor = Color.white;
                    backgroundColor = new Color(0.172549f, 0.3647059f, 0.5294118f);
                }
            }


            EditorGUI.DrawRect(selectionRect, backgroundColor);
            DrawNewLabel(selectionRect, obj, textColor);
            DrawGameObjectIcon(selectionRect, content);
        }

        static void DrawGameObjectIcon(Rect selectionRect, GUIContent content) {
            GUI.DrawTexture(new Rect(selectionRect.xMin, selectionRect.yMin, 16, 16), content.image);
        }

        static void DrawNewLabel(Rect selectionRect, Object obj, Color textColor) {
            var offsetRect = new Rect(selectionRect.position + _offset, selectionRect.size);

           if (PrefabUtility.GetPrefabAssetType(obj as GameObject) == PrefabAssetType.Regular || PrefabUtility.GetPrefabAssetType(obj as GameObject) == PrefabAssetType.Variant || PrefabUtility.GetPrefabAssetType(obj as GameObject) == PrefabAssetType.Model) {
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle {
                    normal = new GUIStyleState {textColor = textColor},
                    fontStyle = FontStyle.Bold
                });
            }
            else {
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle {
                    normal = new GUIStyleState {textColor = textColor},
                });
            }
        }
    }
}