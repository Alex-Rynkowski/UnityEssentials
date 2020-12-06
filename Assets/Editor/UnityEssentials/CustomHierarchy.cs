using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEssentials {
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour {
        public static Color ChangedPrefabColor = new Color(0.7817802f, 0.8f, 0);
        public static Color BackgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f);
        public static Color PrefabTextColor = new Color(0.3843138f, 0.4980392f, 0.6666667f);

        static Vector2 _offset = new Vector2(18, 0);

        static CustomHierarchy() {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGui;
        }

        static void HandleHierarchyWindowItemOnGui(int instanceID, Rect selectionRect) {
            var textColor = new Color(0.454902f, 0.6156863f, 0.854902f);
            var backgroundColor = BackgroundColor;
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);

            if (obj == null || !PrefabUtility.IsAnyPrefabInstanceRoot(obj as GameObject)) return;

            if (PrefabUtility.HasPrefabInstanceAnyOverrides(obj as GameObject, false)) {
                textColor = ChangedPrefabColor;
            }

            var prefabType = PrefabUtility.GetPrefabAssetType(obj);
            if (prefabType == PrefabAssetType.Regular || prefabType == PrefabAssetType.Variant) {
                if (Selection.instanceIDs.Contains(instanceID)) {
                    textColor = Color.white;
                    backgroundColor = new Color(0.172549f, 0.3647059f, 0.5294118f);
                }
            }

            EditorGUI.DrawRect(selectionRect, backgroundColor);
            DrawNewLabel(selectionRect, obj, textColor);
            GUI.DrawTexture(new Rect(selectionRect.xMin, selectionRect.yMin, 16, 16), content.image);
        }

        static void DrawNewLabel(Rect selectionRect, Object obj, Color textColor) {
            var offsetRect = new Rect(selectionRect.position + _offset, selectionRect.size);
            EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle {
                normal = new GUIStyleState {textColor = textColor},
                fontStyle = FontStyle.Bold
            });
        }
    }
}