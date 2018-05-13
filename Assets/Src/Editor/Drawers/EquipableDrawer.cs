using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using EntitySystem;
using Intelligence;

[PropertyDrawerFor(typeof(Equipable))]
public class EquipableDrawer : PropertyDrawerX {

    private SerializedPropertyX rootProperty;
    private SerializedPropertyX equipSlot;
    private SerializedPropertyX weaponCategory;
    private SerializedPropertyX damageType;

    private bool initialized;
    private List<string> skipRenderingFields;

    public override void OnGUI(SerializedPropertyX property, GUIContent label) {
        Initialize(property);
        if((EquipmentSlot)equipSlot.Value == EquipmentSlot.Weapon) {
            if (damageType.Value == null)  {
                damageType.Value = (DamageType)0;
            }
            if (weaponCategory.Value == null) {
                weaponCategory.Value = (WeaponCategory)0;
            }

            skipRenderingFields.Clear();
        } else {
            skipRenderingFields.Add("damageType");
            skipRenderingFields.Add("weaponCategory");

            damageType.Value = null;
            weaponCategory.Value = null;
        }

        EditorGUI.indentLevel++;
        for (int i = 0; i < property.ChildCount; i++) {
            SerializedPropertyX child = property.GetChildAt(i);
            if (skipRenderingFields.IndexOf(child.name) != -1) continue;
            EditorGUILayoutX.PropertyField(child, child.label, child.isExpanded);
        }
        EditorGUI.indentLevel--;
    }

    private void Initialize(SerializedPropertyX source) {
        if (!initialized) {
            equipSlot = source.FindProperty("equipSlot");
            damageType = source.FindProperty("damageType");
            weaponCategory = source.FindProperty("weaponCategory");

            skipRenderingFields = new List<string>();
            initialized = true;
        }
    }
}
