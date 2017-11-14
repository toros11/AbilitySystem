using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using EntitySystem;
using Intelligence;

[PropertyDrawerFor(typeof(ClassList))]
public class ClassListDrawer : PropertyDrawerX {
    private SerializedPropertyX rootProperty;
    private SerializedPropertyX listRoot;
    private bool initialized;
    private ListRenderer listRenderer;
    private List<string> skipRenderingFields;

    public override void OnGUI(SerializedPropertyX property, GUIContent label) {
        Initialize(property);
        EditorGUI.indentLevel++;
        for (int i = 0; i < property.ChildCount; i++) {
            SerializedPropertyX child = property.GetChildAt(i);
            child.isExpanded = true;
            if (skipRenderingFields.IndexOf(child.name) != -1) continue;
            EditorGUILayoutX.PropertyField(child, child.label, child.isExpanded);
        }
        EditorGUI.indentLevel--;
        listRenderer.Render();
    }

    private void Initialize(SerializedPropertyX source) {
        if (!initialized) {
            var tmp = source.GetParent;
            while (tmp != null) {
                rootProperty = tmp;
                tmp = rootProperty.GetParent;
            }

            listRoot = source["classes"];
            listRenderer = new ListRenderer();
            listRenderer.CreateSearchBox = () => {
                var searchSet = Reflector.FindSubClasses(typeof(PlayableClass));
                searchSet = searchSet.FindAll((classType) => {
                        var dummy = Activator.CreateInstance(classType) as PlayableClass;
                        for(int i = 0; i < listRoot.ChildCount; i++) {
                            if(listRoot.GetChildAt(i).Type == dummy.GetType()) return false; 
                        }
                        return true;
                    });
                return new SearchBox(null, searchSet, listRenderer.AddListItem, "Add Class", "Class");
            };

            listRenderer.Initialize();
            listRenderer.SetTargetProperty(rootProperty, listRoot);

            skipRenderingFields = new List<string>();
            skipRenderingFields.Add("classes");
            skipRenderingFields.Add("contextType");

            initialized = true;
        }
    }
}

