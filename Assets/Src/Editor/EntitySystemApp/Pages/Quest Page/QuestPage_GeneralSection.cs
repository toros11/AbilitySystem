using UnityEngine;
using UnityEditor;
using EntitySystem;

public class QuestPage_GeneralSection : SectionBase<Quest> {

    public QuestPage_GeneralSection(float spacing) : base(spacing) { }

    public override void Render() {
        if (rootProperty == null) return;
        SerializedPropertyX isActive = rootProperty.FindProperty("IsActive");

        GUILayout.BeginHorizontal();
        isActive.Value = EditorGUILayout.ToggleLeft(new GUIContent("Is Active"), (bool)isActive.Value);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

    }
}