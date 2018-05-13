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
    public DiceBase diceModifier;
    public float maxLevel;
    public float maxBonus;
    public override void ApplyModifier<T>(T t, ref float inValue) {
        diceCreator = new DiceCreator();
        var totalBonus = 0f;
        var casterLevel = 5f; // ContextEntity.Caster.Parameters.casterLevel;
        var level = MaxValue(casterLevel, maxLevel);
        for (int i = 0; i < level; i++) {
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
        int baseStr = this.user.strength.Value;
        baseStr -= 20;
        baseStr /= 2;
        baseStr /= 2;
        baseStr *= 5;

        inValue = inValue + baseStr;
    }
}

public class OneHandedWeaponModifier : AbilityModifier<SingleTargetContext> {
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
