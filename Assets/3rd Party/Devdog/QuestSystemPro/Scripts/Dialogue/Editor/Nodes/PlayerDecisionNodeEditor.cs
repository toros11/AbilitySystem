using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Devdog.General.Editors.ReflectionDrawers;

namespace Devdog.QuestSystemPro.Dialogue.Editors
{
    [CustomNodeEditor(typeof(PlayerDecisionNode))]
    public class PlayerDecisionNodeEditor : DefaultNodeEditor
    {
        protected int hoveringIndex = -1;

        protected override void DrawFields(ref Rect elementRect)
        {
            hoveringIndex = -1;
            base.DrawFields(ref elementRect);
        }

        protected override void DrawSingleField(ref Rect elementRect, DrawerBase drawer)
        {
            var n = ((PlayerDecisionNode)node);
            maxOutgoingEdges = n.playerDecisions.Length;

            if (ReferenceEquals(drawer.value, n.playerDecisions))
            {
                // TODO: Move to utility class -> Can reuse.
                if (drawer.fieldInfo.FieldType.IsArray)
                {
                    var value = (Array)drawer.value;
                    var arrayDrawer = (ArrayDrawer) drawer;
                    var elem = arrayDrawer.children.FirstOrDefault();

                    var r = elementRect;
                    float height = ReflectionDrawerStyles.singleLineHeight;
                    if (elem != null)
                    {
                        height = elem.GetHeight();
                    }

                    r.height = height;

                    for (int i = 0; i < value.Length; i++)
                    {
                        r.y += height;
                        if (r.Contains(Event.current.mousePosition))
                        {
                            hoveringIndex = i;
                        }
                    }
                }
            }

            base.DrawSingleField(ref elementRect, drawer);
        }

        protected override void NotifyFieldChanged(DrawerBase drawer)
        {
            base.NotifyFieldChanged(drawer);
        }

        protected override void DrawEdgeConnectors()
        {
            int i = 0;
            foreach (var rect in GetEdgeConnectorRects())
            {
                if (i == hoveringIndex)
                {
                    GUI.color = Color.green; // Highlight the hovering decision output connector.
                }

                EditorGUIUtility.AddCursorRect(rect, MouseCursor.ArrowPlus);
                GUI.Button(rect, "", edgeConnectorStyle);

                GUI.color = Color.white;
                i++;
            }
        }
    }
}
