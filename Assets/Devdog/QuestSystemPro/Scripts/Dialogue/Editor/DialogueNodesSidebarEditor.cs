using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Devdog.General;
using UnityEngine;
using UnityEditor;
using EditorStyles = Devdog.General.Editors.EditorStyles;

namespace Devdog.QuestSystemPro.Dialogue.Editors
{
    public class DialogueNodesSidebarEditor : UnityEditor.EditorWindow
    {
        protected static DialogueNodesSidebarEditor window;
//        protected static Type[] availableNodeTypes = new Type[0]; 

        protected static Dictionary<string, List<Type>> lookups = new Dictionary<string, List<Type>>(); 
        protected static List<string> expandedKeys = new List<string>();

        protected static GUIStyle normalButtonStyle;
        protected static GUIStyle selectedButtonStyle;
        protected static string searchQuery;
        protected static bool isSearching
        {
            get { return string.IsNullOrEmpty(searchQuery) == false; }
        }

        public static void Init()
        {
            window = GetWindow<DialogueNodesSidebarEditor>();
            window.minSize = new Vector2(300, 400);

            normalButtonStyle = "ButtonMid";
            selectedButtonStyle = "ButtonMid";

            UpdateAvailableNodeTypes();
        }

        public static void UpdateAvailableNodeTypes()
        {
            var availableNodeTypes = ReflectionUtility.GetAllTypesThatImplement(typeof (NodeBase));
            lookups.Clear();
            foreach (var n in availableNodeTypes)
            {
                var category = (CategoryAttribute)n.GetCustomAttributes(typeof(CategoryAttribute), true).FirstOrDefault();
                var hideInSidebar = (HideInCreationWindowAttribute)n.GetCustomAttributes(typeof(HideInCreationWindowAttribute), true).FirstOrDefault();
                if (hideInSidebar != null)
                {
                    continue;
                }

                string categoryName = "Undefined";
                if (category != null)
                {
                    categoryName = category.category;
                }

                if (lookups.ContainsKey(categoryName) == false)
                {
                    lookups[categoryName] = new List<Type>();
                }

                lookups[categoryName].Add(n);
            }
        }

        protected virtual void OnGUI()
        {
            searchQuery = EditorStyles.SearchBar(searchQuery, this, isSearching);

            foreach (var kvp in lookups)
            {
                if (isSearching)
                {
                    if (kvp.Value.Any(o => o.Name.ToLower().Contains(searchQuery.ToLower())) == false)
                    {
                        continue;
                    }
                }
                else
                {
                    GUI.color = Color.cyan;

                    if (GUILayout.Button(kvp.Key, expandedKeys.Contains(kvp.Key) ? normalButtonStyle : selectedButtonStyle))
                    {
                        if (expandedKeys.Contains(kvp.Key))
                        {
                            expandedKeys.Remove(kvp.Key);
                        }
                        else
                        {
                            expandedKeys.Add(kvp.Key);
                        }
                    }
                }

                GUI.color = Color.white;

                if (expandedKeys.Contains(kvp.Key) || isSearching)
                {
                    foreach (var type in lookups[kvp.Key])
                    {
                        EditorGUILayout.BeginHorizontal();

                        GUILayout.Space(20);
                        if (GUILayout.Button(type.Name))
                        {
                            var node = DialogueEditorWindow.CreateAndAddNodeToCurrentDialog(type);
                            if (node != null)
                            {
                                DevdogLogger.LogVerbose("Add new node of type " + type.Name + " with index: " + node.index);
                            }
                            else
                            {
                                DevdogLogger.Log("Couldn't add node, no dialogue selected.");
                            }
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }

//        protected void DrawCurrentNodeSummary()
//        {
//            
//        }
    }
}