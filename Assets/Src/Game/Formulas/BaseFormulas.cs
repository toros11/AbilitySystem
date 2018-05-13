using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;

public static class BaseFormulas {
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
}
