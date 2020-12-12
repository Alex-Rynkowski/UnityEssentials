using System;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials {
    public class HierarchyEditor : EditorWindow {
        [MenuItem("Tools/Hierarchy Editor")]
        public static void ShowWindow() {
            GetWindow<HierarchyEditor>("HierarchyEditor");
        }

        static bool _setColorsOnStartup;

        Color _backgroundColor, _prefabTextColor, _changedPrefabColor, _missingPrefabColor, _hoverOverBackgroundColor;
        float _red, _green, _blue, _alpha;


        void OnGUI() {
            if (!_setColorsOnStartup) {
                this._backgroundColor = SetupColorsOnStartup("BackgroundColor", 0.2196079f, 0.2196079f, 0.2196079f, 1);
                this._prefabTextColor = SetupColorsOnStartup("PrefabTextColor", 0.3843138f, 0.4980392f, 0.6666667f, 1);
                this._changedPrefabColor = SetupColorsOnStartup("ChangePrefabTextColor", 0.7817802f, 0.8f, 0, 1);
                this._missingPrefabColor = SetupColorsOnStartup("MissingPrefabColor", 1, 0, 0, 1);
                this._hoverOverBackgroundColor = SetupColorsOnStartup("HoverOverColor", 0.2666667f, 0.2666667f, 0.2666667f, 1);
                return;
            }

            CustomHierarchy.ChangedPrefabColor = this._changedPrefabColor;
            CustomHierarchy.BackgroundColor = this._backgroundColor;
            CustomHierarchy.PrefabTextColor = this._prefabTextColor;
            CustomHierarchy.HoverOverBackgroundColor = this._hoverOverBackgroundColor;
            CustomHierarchy.MissingPrefabColor = this._missingPrefabColor;

            try {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space(10);
                this._backgroundColor = EditorGUILayout.ColorField("Prefab Background color", CustomHierarchy.BackgroundColor);
                SaveCustomColors("BackgroundColor", this._backgroundColor.r, this._backgroundColor.g, this._backgroundColor.b, this._backgroundColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._prefabTextColor = EditorGUILayout.ColorField("Prefab text color", CustomHierarchy.PrefabTextColor);
                SaveCustomColors("PrefabTextColor", this._prefabTextColor.r, this._prefabTextColor.g, this._prefabTextColor.b, this._prefabTextColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._missingPrefabColor = EditorGUILayout.ColorField("Missing prefab text color", CustomHierarchy.MissingPrefabColor);
                SaveCustomColors("MissingPrefabColor", this._missingPrefabColor.r, this._missingPrefabColor.g, this._missingPrefabColor.b, this._missingPrefabColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._changedPrefabColor = EditorGUILayout.ColorField("Changed prefab text color", CustomHierarchy.ChangedPrefabColor);
                SaveCustomColors("ChangePrefabTextColor", this._changedPrefabColor.r, this._changedPrefabColor.g, this._changedPrefabColor.b, this._changedPrefabColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._hoverOverBackgroundColor = EditorGUILayout.ColorField("Hover over color bg", CustomHierarchy.HoverOverBackgroundColor);
                SaveCustomColors("HoverOverColor", this._hoverOverBackgroundColor.r, this._hoverOverBackgroundColor.g, this._hoverOverBackgroundColor.b, this._hoverOverBackgroundColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();
            }
            catch (Exception e) {
                // ignored
            }

            if (GUI.Button(new Rect(2, 30 * 6, Screen.width - 2, 25), "Reset Colors to default")) {
                ResetColorsToDefault();
            }
        }

        void ResetColorsToDefault() {
            this._backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f);
            this._prefabTextColor = new Color(0.3843138f, 0.4980392f, 0.6666667f);
            this._changedPrefabColor = new Color(0.7817802f, 0.8f, 0);
            this._missingPrefabColor = Color.red;
            this._hoverOverBackgroundColor = new Color(0.2666667f, 0.2666667f, 0.2666667f);
        }

        Color SetupColorsOnStartup(string playerPrefName, float red, float green, float blue, float alpha) {
            this._red = EditorPrefs.GetFloat(playerPrefName[0] + "0", red);
            this._green = EditorPrefs.GetFloat(playerPrefName[1] + "1", green);
            this._blue = EditorPrefs.GetFloat(playerPrefName[2] + "2", blue);
            this._alpha = EditorPrefs.GetFloat(playerPrefName[3] + "3", alpha);
            _setColorsOnStartup = true;
            return new Color(this._red, this._green, this._blue, this._alpha);
        }

        static void SaveCustomColors(string playerPrefName, float red, float green, float blue, float alpha) {
            EditorPrefs.SetFloat(playerPrefName[0] + "0", red);
            EditorPrefs.SetFloat(playerPrefName[1] + "1", green);
            EditorPrefs.SetFloat(playerPrefName[2] + "2", blue);
            EditorPrefs.SetFloat(playerPrefName[3] + "3", alpha);
        }
    }
}