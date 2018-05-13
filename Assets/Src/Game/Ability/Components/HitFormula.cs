using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class HitFormula : AbilityComponent<Context> {
    public bool debugMode;
    public DiceBase diceInputValue;
    public float outputValue;
    public CharacterCreator user;
    public CharacterCreator target;
    public MethodPointer<Context, Character, Character, float> hitFormula;

    public override void OnUse() {
        if (this.user == null || this.target == null) return;
        // temporary
        var u = MockCharacter(user);
        var t = MockCharacter(target);

        if(debugMode) {
            hitFormula.OnAfterDeserialize();
        }
        if (hitFormula.PointsToMethod) {
            outputValue = (float)hitFormula.Invoke(this.context, u, t);
        }
    }

    private Character MockCharacter(CharacterCreator character) {
        var c = character.Create();
        var equipTable = new InventoryItemCreator[] {
            c.equipment.body,
            c.equipment.weapon,
        };

        return c;
    }
}
