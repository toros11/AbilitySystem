using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormulas {
    [Pointable]
    public static float Fn(float f) {
        return 0;
    }

    [Pointable]
    public static float Slash(SingleTargetContext context, float baseValue) {
        return 10f;
    }

    [Pointable]
    public static float ShadowSlash(SingleTargetContext context, float baseValue) {
        return 11;
    }

    [Pointable]
    public static float Strike(SingleTargetContext context, float baseValue) {
        return (baseValue + 10.0f);
    }

    [Pointable]
    public static DiceBase GetWeaponDice(Context context) {
        return DiceBase.BASE_10;
    }

    [Pointable]
    public static int GetCharacterSize(Context context) {
        return -1;
    }
}

