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
    public GameObject user;
    public GameObject target;
    public MethodPointer<Context, float> hitFormula;

    public override void OnUse() {
        if (this.user == null || this.target == null) return;

        #if UNITY_EDITOR
        CreateMockContext();
        #endif

        if(debugMode) {
            hitFormula.OnAfterDeserialize();
        }

        if (hitFormula.PointsToMethod) {
            outputValue = (float)hitFormula.Invoke(this.context);
        }
    }

    private void CreateMockContext() {
        this.context = new SingleTargetContext(user.GetComponent<Entity>(),
                                               target.GetComponent<Entity>());
        this.context.entity.Awake();
        this.context.entity.characterManager.Init();
    }
}
