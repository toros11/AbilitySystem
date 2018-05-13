using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;

public static class Formulas {
    [Pointable]
    public static DiceBase GetWeaponDice(Context context) {
        return DiceBase.BASE_10;
    }

    [Pointable]
    public static int GetCharacterSize(Context context) {
        return -1;
    }

    [Pointable]
    public static float MeleeHitChance(Context context, BaseParameters user, BaseParameters target) {
        var hitChance = BaseFormulas.GetStrBonus(user.strength.Value);
        return (float)(hitChance * 5);
    }

    [Pointable]
    public static float MagicHitChance(Context context, BaseParameters user, BaseParameters target) {
        return -1;
    }

    [Pointable]
    public static float RangeHitChance(Context context, BaseParameters user, BaseParameters target) {
        return -1;
    }

    [Pointable]
    public static float AlwaysHit(Context context, BaseParameters user, BaseParameters target) {
        return 99999;
    }

}
