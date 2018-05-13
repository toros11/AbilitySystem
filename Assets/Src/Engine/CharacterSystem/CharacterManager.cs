// using System;
using UnityEngine;
using System.Collections.Generic;
using AbilitySystem;
using Intelligence;

namespace EntitySystem {
    public class CharacterManager {
        protected Entity entity;
        protected GameObject gameObj;
        protected Character character;
        protected Decision des;

        public CharacterManager(Entity entity, CharacterCreator characterAsset) {
            this.entity = entity;
            this.character = characterAsset.Create();
            if(character.isPlayer) {
                entity.transform.position = new Vector3(-2, 0);
            } else {
                entity.transform.position = new Vector3(2, 0);
            }
        }

        public void Init() {
            SetSkills();
            SetGear();
            if(!character.isPlayer) {
                if(character.decisionPackage != null) {
                    var d = character.decisionPackage.Create();
                    des = d.decisions[0];
                }
            }
        }

        private void CheckInput() {
            if(Input.GetMouseButtonDown(0) && character.isPlayer) {
                entity.ActiveEquipment[(int)EquipmentSlot.Weapon].Use();
            }
        }

        public void Spawn() {
            if(character.prefab == null) return;

            gameObj = null;
            Vector3 position = entity.transform.position;
            Quaternion rotation = Quaternion.identity;
            PointContext context = new PointContext(entity, position);
            gameObj = Object.Instantiate(character.prefab, position, rotation) as GameObject;
            IContextAware[] components = gameObj.GetComponents<IContextAware>();
            if (components != null) {
                for (int i = 0; i < components.Length; i++) {
                    components[i].SetContext(context);
                }
            }
        }

        private void SetSkills() {
            if(character.parameters.baseParameters.classList == null) return;
            var classes = character.parameters.baseParameters.classList.classes;
            for(int i = 0; i < classes.Count ; i++) {
                var c = classes[i];
                if (c == null || c.abilities == null || c.abilities.Length == 0) continue;
                for(int j = 0; j < c.abilities.Length; j++) {
                    var skill = c.abilities[j].Create();
                    var entry = new KeyValuePair<GameClass, Ability>(c.GameClass, skill);
                    entity.SkillBook.Add(entry);
                    entity.SkillBook.Last().Value.Caster = entity;
                }
            }
        }

        private void SetGear() {
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

            for (int i = 0; i < equipTable.Length; i++) {
                if (equipTable[i] != null) {
                    var item = equipTable[i].Create();
                    item.isEquipable = true;
                    entity.itemManager.EquipItem(item, (EquipmentSlot)i);
                }
            }
        }

        public List<KeyValuePair<GameClass, Ability>> SkillBook {
            get {
                return character.skillBook;
            }
        }

        public Character Character {
            get {
                return character;
            }
        }

        public void Update() {
            CheckInput();
            if(des != null) {
                des.action.OnStart();
            }
        }
    }
}
