using EntitySystem;
using UnityEngine;
using UnityEditor;
using System;

public class CharacterPage_ParamSection : RandomGeneratorSection<Character> {

    public CharacterPage_ParamSection(float spacing) : base(spacing) { }
    private Type lastContextType;

    protected override string FoldOutLabel {
        get { return "Parameters"; }
    }

    protected override string ListRootName {
        get { return "parameters"; }
    }

    protected override void RenderBody(SerializedPropertyX property, RenderData data, int index) {
        EditorGUI.indentLevel++;
        for (int i = 0; i < property.ChildCount; i++) {
            SerializedPropertyX child = property.GetChildAt(i);
            child.isExpanded = true;
            if (skipRenderingFields.IndexOf(child.name) != -1) continue;
            EditorGUILayoutX.PropertyField(child, child.label, child.isExpanded);
        }
        EditorGUI.indentLevel--;
    }


    public override void CreateRollButton() {
        if (GUILayout.Button("Roll Random")) {
            SerializedPropertyX baseParams = rootProperty.FindProperty("parameters").FindProperty("baseParameters");
            System.Random rand = new System.Random();
            int sum = 0;

            for (int i = 2 ; i < baseParams.ChildCount; i++) {
                SerializedPropertyX childProp = baseParams.GetChildAt(i).FindProperty("baseValue");
                var low = Math.Min(rand.Next(7, 15), rand.Next(3,18));
                var high = Math.Max(rand.Next(9, 17), rand.Next(6, 18));
                var randomRoll = (int)rand.Next(low, Mathf.Clamp(high, low, 18));
                childProp.Value = randomRoll;
                sum += randomRoll;
            }
            Debug.Log("Total:" + sum);            
        }
    }

    public override void Render() {
        base.Render();
    }
}
