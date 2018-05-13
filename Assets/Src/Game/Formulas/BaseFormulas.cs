using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;

public static class BaseFormulas {
    // Formulas for base parameters
    public static int GetBaseStat(int i) {
        var val = i - 20;
        val = val / 2;
        val = val / 2;

        return val;
    }
    public static int GetStrBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    public static int GetAgiBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    public static int GetConBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    public static int GetIntBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    public static int GetWisBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    public static int GetChrBonus(int i) {
        return BaseFormulas.GetBaseStat(i);
    }

    // Fromula for weapon types

    public static int GetWeaponBonus(InventoryItem weapon) {
        return -1;
    }


    public static List<PlayableClass> GetClasses(Character c) {
        var classList = c.parameters.baseParameters.classList.classes;
        if (classList.Count > 1){
            Debug.LogWarning("Multiple classes not fully supported yet, might break");
        }

        return classList;
    }
}
