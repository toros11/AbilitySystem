using System.Collections.Generic;

public class DiceCreator {

    public readonly Dictionary<DiceBase, DiceData> DiceTable = new Dictionary<DiceBase, DiceData> {
        { DiceBase.BASE_2d2, new DiceData(1, 2) },
        { DiceBase.BASE_2d4, new DiceData(1, 4) },
        { DiceBase.BASE_2d6, new DiceData(1, 6) },
        { DiceBase.BASE_2d8, new DiceData(1, 8) },
        { DiceBase.BASE_2d10, new DiceData(1, 10) },
        { DiceBase.BASE_2d12, new DiceData(1, 12) },
        { DiceBase.BASE_4d6, new DiceData(2, 6) },
        { DiceBase.BASE_6d6, new DiceData(3, 6) },
        { DiceBase.BASE_2, new DiceData(1)},
        { DiceBase.BASE_4, new DiceData(2)},
        { DiceBase.BASE_6, new DiceData(3)},
        { DiceBase.BASE_8, new DiceData(4)},
        { DiceBase.BASE_10, new DiceData(5)},
    };

    public DiceData this[DiceBase baseValue] {
        get {
            DiceData dice = DiceTable[baseValue];
            var r = new System.Random();
            int[] results = new int[dice.RollCnt * 2];
            for(int i = 0; i < (dice.RollCnt * 2); i++) {
                results[i] = r.Next(dice.MinValue, dice.MaxValue + 1);
            }
            return dice.Final(results);
        }
    }

    public DiceData GenerateDiceResult(DiceData dice, int extraRoll = 0) {
        var r = new System.Random();
        int[] results = new int[dice.RollCnt * 2];
        for(int i = 0; i < (dice.RollCnt * 2) + extraRoll; i++) {
            results[i] = r.Next(dice.MinValue, dice.MaxValue + 1);
        }
        return dice.Final(results);
    }
}
