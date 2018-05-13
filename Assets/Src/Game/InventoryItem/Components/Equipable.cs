using UnityEditor;
using UnityEngine;
using Intelligence;
using EntitySystem;

public class Equipable : InventoryItemComponent {

    public EquipmentSlot equipSlot;
    public WeaponCategory weaponCategory;
    public DamageType damageType;
    public BaseParameters requiredParameters;
    public BaseParameters modifies;
    public BaseParameters setParamsTo;

    public override void OnEquip() {
        Debug.Log("weapon category" + weaponCategory);
        Debug.Log("damageType category" + damageType);
        // item.Owner.Parameters.baseParameters.strength.SetModifier("Protein Powder", FloatModifier.Percent(50.2f));
    }

    public override void OnRemove() {

    }
}
