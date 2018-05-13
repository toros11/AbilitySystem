using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using EntitySystem;
using Intelligence;

[PropertyDrawerFor(typeof(HitFormula))]
public class HitFormulaDrawer : PropertyDrawerX {

    private SerializedPropertyX rootProperty;
    private SerializedPropertyX debugMode;
    private bool initialized;
    private List<string> skipRenderingFields;

    public override void OnGUI(SerializedPropertyX property, GUIContent label) {
        Initialize(property);
        if((bool)debugMode.Value) {
            EditorGUI.indentLevel++;
            for (int i = 0; i < property.ChildCount; i++) {
                SerializedPropertyX child = property.GetChildAt(i);
                child.isExpanded = true;
                if (skipRenderingFields.IndexOf(child.name) != -1) continue;
                EditorGUILayoutX.PropertyField(child, child.label, child.isExpanded);
            }
            EditorGUI.indentLevel--;
            CalculateButton(property);
        } else {
            EditorGUI.indentLevel++;
            EditorGUILayoutX.PropertyField(debugMode, debugMode.label, debugMode.isExpanded);
            EditorGUI.indentLevel--;
        }
    }

    private void Initialize(SerializedPropertyX source) {
        if (!initialized) {
            var tmp = source.GetParent;
            while (tmp != null) {
                rootProperty = tmp;
                tmp = rootProperty.GetParent;
            }

            skipRenderingFields = new List<string>();
            skipRenderingFields.Add("contextType");

            debugMode = source.FindProperty("debugMode");
            initialized = true;
        }
    }

    private void CalculateButton(SerializedPropertyX property) {
        property.ApplyModifiedProperties();
        property.Update();

        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Calculate Formula", GUILayout.Width(150))) {
            HitFormula d = property.GetValue<HitFormula>();
            d.OnUse();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
