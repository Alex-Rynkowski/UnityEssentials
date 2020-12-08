using System;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials {
    public class HierarchyEditor : EditorWindow {
        [MenuItem("Tools/Hierarchy Editor")]
        public static void ShowWindow() {
            GetWindow<HierarchyEditor>("HierarchyEditor");
        }

        static bool _setColorsOnStartup;

        Color _backgroundColor, _prefabTextColor, _changedPrefabColor, _hoverOverBackgroundColor;
        float _red, _green, _blue, _alpha;


        void OnGUI() {
            if (!_setColorsOnStartup) {
                this._backgroundColor = SetupColorsOnStartup("BackgroundColor", 0.2196079f, 0.2196079f, 0.2196079f, 1);
                this._prefabTextColor = SetupColorsOnStartup("PrefabTextColor", 0.3843138f, 0.4980392f, 0.6666667f, 1);
                this._changedPrefabColor = SetupColorsOnStartup("ChangePrefabTextColor", 0.7817802f, 0.8f, 0, 1);
                this._hoverOverBackgroundColor = SetupColorsOnStartup("HoverOverColor", 0.2666667f, 0.2666667f, 0.2666667f, 1);
                return;
            }

            CustomHierarchy.ChangedPrefabColor = this._changedPrefabColor;
            CustomHierarchy.BackgroundColor = this._backgroundColor;
            CustomHierarchy.PrefabTextColor = this._prefabTextColor;
            CustomHierarchy.HoverOverBackgroundColor = this._hoverOverBackgroundColor;

            try {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space(10);
                this._backgroundColor = EditorGUILayout.ColorField("Prefab Background Color", CustomHierarchy.BackgroundColor);
                SaveCustomColors("BackgroundColor", this._backgroundColor.r, this._backgroundColor.g, this._backgroundColor.b, this._backgroundColor.a);
                EditorGUILayout.Space(10);

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._prefabTextColor = EditorGUILayout.ColorField("Prefab text Color", CustomHierarchy.PrefabTextColor);
                SaveCustomColors("PrefabTextColor", this._prefabTextColor.r, this._prefabTextColor.g, this._prefabTextColor.b, this._prefabTextColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();


                EditorGUILayout.BeginVertical();
                this._changedPrefabColor = EditorGUILayout.ColorField("Changed prefab text color", CustomHierarchy.ChangedPrefabColor);
                SaveCustomColors("ChangePrefabTextColor", this._changedPrefabColor.r, this._changedPrefabColor.g, this._changedPrefabColor.b, this._changedPrefabColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                this._hoverOverBackgroundColor = EditorGUILayout.ColorField("Hover over color background", CustomHierarchy.HoverOverBackgroundColor);
                SaveCustomColors("HoverOverColor", this._hoverOverBackgroundColor.r, this._hoverOverBackgroundColor.g, this._hoverOverBackgroundColor.b, this._hoverOverBackgroundColor.a);
                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();
            }
            catch (Exception e) {
                // ignored
            }

            if (GUI.Button(new Rect(2, 30 * 5, Screen.width - 2, 25), "Reset Colors to default")) {
                ResetColorsToDefault();
            }
        }

        void ResetColorsToDefault() {
            this._backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f);
            this._prefabTextColor = new Color(0.3843138f, 0.4980392f, 0.6666667f);
            this._changedPrefabColor = new Color(0.7817802f, 0.8f, 0);
            this._hoverOverBackgroundColor = new Color(0.2666667f, 0.2666667f, 0.2666667f);
        }

        Color SetupColorsOnStartup(string playerPrefName, float red, float green, float blue, float alpha) {
            this._red = PlayerPrefs.GetFloat(playerPrefName[0] + "0", red);
            this._green = PlayerPrefs.GetFloat(playerPrefName[1] + "1", green);
            this._blue = PlayerPrefs.GetFloat(playerPrefName[2] + "2", blue);
            this._alpha = PlayerPrefs.GetFloat(playerPrefName[3] + "3", alpha);
            _setColorsOnStartup = true;
            return new Color(this._red, this._green, this._blue, this._alpha);
        }

        static void SaveCustomColors(string playerPrefName, float red, float green, float blue, float alpha) {
            PlayerPrefs.SetFloat(playerPrefName[0] + "0", red);
            PlayerPrefs.SetFloat(playerPrefName[1] + "1", green);
            PlayerPrefs.SetFloat(playerPrefName[2] + "2", blue);
            PlayerPrefs.SetFloat(playerPrefName[3] + "3", alpha);
        }
    }
}