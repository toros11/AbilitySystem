using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;


public class ContextModifier : Modifier<Context> {
    public int test;
    public WeaponCategroy weaponCat;
    public ArmorType armorCat;
    public MethodPointer<Context, float> formula;

    public override void ApplyModifier(ref float value) {
        if(armorCat == ArmorType.Plate || armorCat == ArmorType.Chain) {
            value *= 2;
        }
    }
}

public class CharacterSizeBonus : Modifier<Context> {
    public int bonusDice;
    public override void ApplyModifier(ref float value) {
    }
}

public class CharacterLevelBonus : Modifier<Context> {
    public DiceBase diceModifier;
    public float maxLevel;
    public float maxBonus;
    public override void ApplyModifier(ref float value) {
        diceCreator = new DiceCreator();
        var totalBonus = 0f;
        // var level = context.entity.Parameters.casterLevel;
        var level = 6f;
        if (level > maxLevel && maxLevel > 0) level = maxLevel;
        for (int i = 0; i < level; i++) {
            totalBonus += (float)diceCreator[diceModifier].Result;
        }
        if(totalBonus > maxBonus && maxBonus > 0) totalBonus = maxBonus;
        value += totalBonus; 
    }
}

public class StrModifier : Modifier<Context> {
    public float bonus;
    public override void ApplyModifier(ref float inValue) {
        inValue = inValue * bonus;
    }
}

public class OneHandedWeaponModifier : Modifier<SingleTargetContext> {
    public float test;
    public MethodPointer<SingleTargetContext, float, float> formula;
    public override void ApplyModifier(ref float inValue) {
        // while testing
        // if(debugMode) formula.OnAfterDeserialize();
        inValue = formula.Invoke((SingleTargetContext)this.context, inValue + test);
    }
}

public class OneModifier : Modifier<MultiPointContext> {
    public MethodPointer<MultiPointContext, float, float> formula;
    public override void ApplyModifier(ref float inValue) {
    }
}

public class TwoModifier : Modifier<DirectionalContext> {
    public MethodPointer <DirectionalContext, float, float, float> formula;
    public override void ApplyModifier(ref float inValue) {
    }
}

