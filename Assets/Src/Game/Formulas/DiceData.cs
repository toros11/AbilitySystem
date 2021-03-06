using System.Collections.Generic;

public enum DiceBase {
    // dice roll types
    BASE_2d2 = 0,
    BASE_2d4 = 1,
    BASE_2d6 = 2,
    BASE_2d8 = 3,
    BASE_2d10 = 4,
    BASE_2d12 = 5,
    BASE_4d6 = 6,
    BASE_6d6 = 7,
    // static value dices
    BASE_0 = 8,
    BASE_2 = 9,
    BASE_4 = 10,
    BASE_6 = 11,
    BASE_8 = 12,
    BASE_10 = 13,
}

public struct DiceData {
    private int _maxValue;
    private int _minValue;
    private int _rollCount;
    private int[] _finalValue;

    public int MaxValue { get { return _maxValue; } }
    public int MinValue { get { return _minValue; } }
    public int RollCnt { get { return _rollCount; } }

    public int Result {
        get {
            int sum = 0;
            for(int i = 0; i < _finalValue.Length; i++) sum += _finalValue[i];
            return sum;
        }
    }

    public DiceData(int value) {
        _rollCount = 1;
        _maxValue = value;
        _minValue = value;
        _finalValue = new int[1];
    }

    public DiceData(int cnt, int max, int min = 1) {
        _maxValue = max;
        _minValue = min;
        _rollCount = cnt;
        _finalValue = new int[_rollCount];
    }

    public DiceData(int cnt, int max, int min, int[] final) {
        _minValue =  min;
        _maxValue = max;
        _finalValue = final;
        _rollCount = cnt;
    }

    public DiceData Final(int[] values) {
        return new DiceData(_rollCount, _maxValue, _minValue, values);
    }
}
