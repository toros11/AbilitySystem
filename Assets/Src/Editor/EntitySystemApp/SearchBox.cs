﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SearchBox {

    private Texture2D blue;
    private bool hasResults;
    private List<Type> results;
    private bool searching;
    private string searchString;
    private List<Type> searchSet;
    private GUIContent resultContent;
    private Action<Type> selected;
    private string buttonText;
    private string labelText;

    public SearchBox(Texture2D resultIcon, Type searchType, Action<Type> selectedAction, string buttonText, string labelText) {
        searchString = string.Empty;
        searchSet = Reflector.FindSubClasses(searchType);
        this.buttonText = buttonText;
        this.labelText = labelText;
        results = new List<Type>();
        resultContent = new GUIContent();
        resultContent.image = resultIcon ?? EditorGUIUtility.FindTexture("cs ScriptIcon");
        selected = selectedAction;
        blue = new Texture2D(1, 1);
        blue.SetPixel(0, 0, Color.blue);
        blue.Apply();
    }

    public SearchBox(Texture2D resultIcon, List<Type> searchSet, Action<Type> selectedAction, string buttonText, string labelText) {
        searchString = string.Empty;
        this.searchSet = searchSet;
        this.buttonText = buttonText;
        this.labelText = labelText;
        results = new List<Type>();
        resultContent = new GUIContent();
        resultContent.image = resultIcon ?? EditorGUIUtility.FindTexture("cs ScriptIcon");
        selected = selectedAction;
        blue = new Texture2D(1, 1);
        blue.SetPixel(0, 0, Color.blue);
        blue.Apply();
    }

    public void Render() {

        if (!searching && GUILayout.Button(buttonText, GUILayout.Width(225))) {
            searching = !searching;
        }

        if (searching) {

            GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(225));
            GUILayout.Box(labelText, new GUIStyle(EditorStyles.helpBox) {
                font = EditorStyles.boldLabel.font,
                alignment = TextAnchor.UpperCenter,
            });

            EditorGUILayout.BeginHorizontal();
            searchString = GUILayout.TextField(searchString);
            if (GUILayout.Button("X", GUI.skin.GetStyle("minibutton"), GUILayout.Width(25f))) {
                searching = false;
                searchString = string.Empty;
                results.Clear();
            }
            EditorGUILayout.EndHorizontal();

            results.Clear();
            results.AddRange(searchSet.FindAll((type) => {
                return type.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1;
            }));

            if (results.Count > 10) {
                results.RemoveRange(10, results.Count - 10);
            }
            GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
            itemStyle.normal.background = null;
            itemStyle.hover.background = blue;
            itemStyle.active.background = blue;
            itemStyle.alignment = TextAnchor.MiddleLeft;
            for (int i = 0; i < results.Count; i++) {
                resultContent.text = results[i].Name;
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(resultContent, itemStyle, GUILayout.MaxHeight(18f))) {
                    searching = false;
                    searchString = string.Empty;
                    selected(results[i]);
                    results.Clear();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }
}