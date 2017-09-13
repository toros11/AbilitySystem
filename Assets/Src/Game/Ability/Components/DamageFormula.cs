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
        var finalInput = FinalizeDice();
        var sum = (float)diceCreator[finalInput].Result;
        for(int i = 0; i < modifiers.Count; i++) {
            var j = sum;
            modifiers[i].SetContext(this.context);
            modifiers[i].ApplyModifier(ref sum);
            if(debugMode) Debug.Log("Apply modifier:"+ modifiers[i].GetType() + " [ input value: " + j + " => " + sum + "]");
        }
        outputValue = sum;
        // TODO: use this output in a way that makes more sense
    }

    private DiceBase FinalizeDice() {
        int newBase;
        int sizeMod = 0;

        if(debugMode) {
            size.OnAfterDeserialize();
            inputFormula.OnAfterDeserialize();
        }
        if (size.PointsToMethod) {
            sizeMod = size.Invoke(this.context);
        }
        if (inputFormula.PointsToMethod) {
            newBase = (int)inputFormula.Invoke(this.context) + sizeMod;
        } else {
            newBase = (int)inputValue + sizeMod;
        }

        if(newBase <= 0) newBase = 0;
        return (DiceBase)(int)Mathf.Max(0,Mathf.Min(newBase,(int)DiceBase.BASE_6d6));
    }
}
