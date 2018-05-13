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
    public static float PhysHitChance(Context context, Character user, Character target) {
        var weapon = context.entity.ActiveEquipment[(int)EquipmentSlot.Weapon];
        var weaponBonus = BaseFormulas.GetWeaponBonus(weapon);
        return -1;
    }

    [Pointable]
    public static float MeleeHitChance(Context context, Character user, Character target) {
        var baseParams = user.parameters.baseParameters;
        float hitChance = BaseFormulas.GetStrBonus(baseParams.strength.Value);
        hitChance = hitChance * 5;
        return hitChance;
    }

    [Pointable]
    public static float MagicHitChance(Context context, Character user, Character target) {
        return -1;
    }

    [Pointable]
    public static float RangeHitChance(Context context, Character user, Character target) {
        return -1;
    }

    [Pointable]
    public static float AlwaysHit(Context context, Character user, Character target) {
        return 99999;
    }

}
