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
    public static float PhysHitChance(Context context) {
        int[] sizeTable = { 12, 6 , 0, -6, 12 };
        Dictionary<GameClass, float> classTable = new Dictionary<GameClass, float> {
            { GameClass.Assassin, 3.75f },
            { GameClass.Warrior, 5f },
            { GameClass.Cleric, 3.75f },
            { GameClass.Warlock, 2.5f },
            { GameClass.Ranger, 5f },
        };

        var baseParams = context.entity.Parameters.baseParameters;
        var classes = BaseFormulas.GetClasses(context.entity.characterManager.Character);
        var weapon = context.entity.ActiveEquipment[(int)EquipmentSlot.Weapon];

        var stats = BaseFormulas.GetStrBonus(baseParams.strength.Value);
        if (weapon.GetInventoryItemComponent<Equipable>().weaponCategory == WeaponCategory.Ranged) {
            stats = BaseFormulas.GetAgiBonus(baseParams.agility.Value);
        }

        float classBonus = (classTable[classes[0].GameClass] * classes[0].level);
        float statsBonus = (5 * stats);
        float sizeBonus = sizeTable[(int)context.entity.Parameters.raceSize];
        float toHitModifier = 0;

        return (Mathf.Round(classBonus) +
                statsBonus +
                sizeBonus +
                toHitModifier);
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
