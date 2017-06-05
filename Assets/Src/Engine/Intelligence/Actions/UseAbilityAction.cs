using Intelligence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityAction : CharacterAction<Context> {
    public string abilityId;

    public override CharacterActionStatus OnUpdate() {
        entity.abilityManager.Cast(abilityId, context);

        return entity.abilityManager.IsCasting ? CharacterActionStatus.Running : CharacterActionStatus.Completed;
    }

    public override void OnCancel() {
        entity.abilityManager.CancelCast();
    }
}
