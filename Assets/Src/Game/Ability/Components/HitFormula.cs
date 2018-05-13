using Intelligence;
using EntitySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class HitFormula : AbilityComponent<Context> {
    public bool debugMode;
    public DiceBase diceInputValue;
    public float outputValue;
    public CharacterCreator user;
    public CharacterCreator target;
    public MethodPointer<Context, BaseParameters, BaseParameters, float> hitFormula;

    public override void OnUse() {
        if (this.user == null || this.target == null) return;

        var userParams = user.Create().parameters.baseParameters;
        var targetParams = target.Create().parameters.baseParameters;

        if(debugMode) {
            hitFormula.OnAfterDeserialize();
            }
        if (hitFormula.PointsToMethod) {
            outputValue = (float)hitFormula.Invoke(this.context, userParams, targetParams);
        }


        // var diceCreator = new DiceCreator();
        // var finalizedInput = FinalizeDice((float)diceInputValue);
        // var sum = (float)diceCreator[finalizedInput].Result;

        // for(int i = 0; i < modifiers.Count; i++) {
        //     var j = sum;
        //     modifiers[i].SetContext(this.context);
        //     modifiers[i].SetParameters(userParams);
        //     modifiers[i].SetTargetParams(targetParams);
        //     modifiers[i].ApplyModifier<Ability>(ability, ref sum);
        //     if(debugMode)
        //         Debug.Log("Apply modifier:"+ modifiers[i].GetType() + " [ input value: " + j + " => " + sum + "]");
        // }
        // output
        // Value = sum;
        // TODO: use this output in a way that makes more sense
    }

    // private DiceBase FinalizeDice(float input) {
    //     float newBase = input;
    //     float sizeMod = 0;

    //     if(debugMode) {
    //         size.OnAfterDeserialize();
    //         inputFormula.OnAfterDeserialize();
    //     }
    //     if (inputFormula.PointsToMethod) {
    //         input = (float)inputFormula.Invoke(this.context);
    //     }
    //     if (size.PointsToMethod) {
    //         newBase = input + size.Invoke(this.context);
    //         if (input < (float)DiceBase.BASE_0) {
    //             newBase = Mathf.Max(0,Mathf.Min(newBase,(float)DiceBase.BASE_6d6));
    //         } else {
    //             newBase = Mathf.Max((float)DiceBase.BASE_0,Mathf.Min(newBase,(float)DiceBase.BASE_10));
    //         }
    //     }
    //     return (DiceBase)newBase;
    // }
}
