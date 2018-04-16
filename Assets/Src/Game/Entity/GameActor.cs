using UnityEngine;
using Intelligence;
using EntitySystem;
using System.Collections.Generic;

namespace EntitySystem {
    public class GameActor : Entity {
        [SerializeField] private CharacterCreator characterCreator;

        private PlayerCharacterAction skillAction;

        public override void Init() {
            character = characterCreator.Create();
            var equipTable = new InventoryItemCreator[] {
                character.equipment.head,
                character.equipment.shoulder,
                character.equipment.feet,
                character.equipment.body,
                character.equipment.legs,
                character.equipment.neck,
                character.equipment.gloves,
                character.equipment.waist,
                character.equipment.ring1,
                character.equipment.ring2,
                character.equipment.weapon,
            };

            var allClasses = character.parameters.baseParameters.classList.classes;
            for(int i = 0; i < allClasses.Count ; i++) {
                var c = allClasses[i];
                for(int j = 0; j < c.abilities.Length; j++) {
                    SkillBook.Add(new KeyValuePair<GameClass, Ability>(c.GameClass, c.abilities[j].Create()));
                    SkillBook.Last().Value.Caster = this;
                }
            }

            for (int i = 0; i < equipTable.Length; i++) {
                if (equipTable[i] != null) {
                    var item = equipTable[i].Create();
                    item.isEquipable = true;
                    itemManager.EquipItem(item, (EquipmentSlot)i);
                }
            }

            ActiveEquipment[(int)EquipmentSlot.Weapon].Use();
        }
    }
}
