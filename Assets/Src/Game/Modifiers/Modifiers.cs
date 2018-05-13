using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;


public class ContextModifier : Modifier<Context> {
    public int test;
    public WeaponCategroy weaponCat;
    public ArmorType armorCat;
    public MethodPointer<Context, float> formula;

    public override void ApplyModifier<T>(T t, ref float inValue) {
        if(armorCat == ArmorType.Plate || armorCat == ArmorType.Chain) {
            inValue *= 2;
        }
    }
}

public class CharacterSizeBonus : AbilityModifier<Context> {
    public int bonusDice;
    public override void ApplyModifier<T>(T t, ref float inValue) {
    }
}

public class CharacterLevelBonus : AbilityModifier<Context> {
    public int interval = 1;
    public DiceBase diceModifier;
    public float maxLevel;
    public float maxBonus;
    public float casterLevel;

    public override void ApplyModifier<T>(T t, ref float inValue) {
        diceCreator = new DiceCreator();

        var totalBonus = 0f;
        var level = MaxValue(casterLevel, maxLevel);
        for (int i = 0; i < level; i += interval) {
            totalBonus += (float)diceCreator[diceModifier].Result;
        }
        inValue += MaxValue(totalBonus, maxBonus);
    }

    private float MaxValue(float value, float maxValue) {
        return (maxValue > 0) ? Mathf.Min(value, maxValue) : value;
    }
}

public class HpModifier : AbilityModifier<Context> {
    public override void ApplyModifier<T>(T t, ref float inValue) {
        if (this.user != null) {
            inValue = inValue + (this.user.constitution.Value);
        }
    }
}

public class StrModifier : AbilityModifier<Context> {
    public override void ApplyModifier<T>(T t, ref float inValue) {
        inValue = inValue + BaseFormulas.GetStrBonus(this.user.strength.Value);
    }
}



public class OneHandedWeaponModifier : AbilityModifier<Context> {
    public float test;
    public MethodPointer<SingleTargetContext, float, float> formula;
    public override void ApplyModifier<T>(T t, ref float inValue) {
        // while testing
        // if(debugMode) formula.OnAfterDeserialize();
        inValue = formula.Invoke((SingleTargetContext)this.context, inValue + test);
    }
}

public class OneModifier : AbilityModifier<MultiPointContext> {
    public MethodPointer<MultiPointContext, float, float> formula;
    public override void ApplyModifier<T>(T t, ref float inValue) {
    }
}

public class TwoModifier : AbilityModifier<DirectionalContext> {
    public MethodPointer <DirectionalContext, float, float, float> formula;
    public override void ApplyModifier<T>(T t, ref float inValue) {
    }
}
