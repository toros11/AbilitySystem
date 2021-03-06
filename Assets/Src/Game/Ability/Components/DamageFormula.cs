using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DamageFormula : AbilityComponent<Context> {
    public bool debugMode;
    public List<Modifier> modifiers;
    public DiceBase inputValue;
    public MethodPointer<Context, DiceBase> inputFormula;
    public MethodPointer<Context, int> size;
    public float outputValue;

    public override void OnUse() {
        var diceCreator = new DiceCreator();
        var finalizedInput = FinalizeDice((float)inputValue);
        var sum = (float)diceCreator[finalizedInput].Result;

        for(int i = 0; i < modifiers.Count; i++) {
            var j = sum;
            modifiers[i].SetContext(this.context);
            modifiers[i].ApplyModifier<Ability>(ability, ref sum);
            if(debugMode)
                Debug.Log("Apply modifier:"+ modifiers[i].GetType() + " [ input value: " + j + " => " + sum + "]");
        }
        outputValue = sum;
        // TODO: use this output in a way that makes more sense
    }

    private DiceBase FinalizeDice(float input) {
        float newBase = input;
        float sizeMod = 0;

        if(debugMode) {
            size.OnAfterDeserialize();
            inputFormula.OnAfterDeserialize();
        }
        if (inputFormula.PointsToMethod) {
            input = (float)inputFormula.Invoke(this.context);
        }
        if (size.PointsToMethod) {
            newBase = input + size.Invoke(this.context);
            if (input < (float)DiceBase.BASE_0) {
                newBase = Mathf.Max(0,Mathf.Min(newBase,(float)DiceBase.BASE_6d6));
            } else {
                newBase = Mathf.Max((float)DiceBase.BASE_0,Mathf.Min(newBase,(float)DiceBase.BASE_10));
            }
        }
        return (DiceBase)newBase;
    }
}
