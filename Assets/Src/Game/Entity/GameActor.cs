// using UnityEngine;
// using Intelligence;
// using EntitySystem;
// using System.Collections.Generic;


// // todo perhaps move this to engine and expose only the entity class to unity
// // and handle character in some sort of manager
// namespace EntitySystem {
//     public class GameActor : Entity {
//         private PlayerCharacterAction skillAction;

//         [SerializeField] private CharacterCreator characterCreator;

//         public override void Init() {
//             character = characterCreator.Create();
//             if(character.isPlayer) {
//                 this.transform.position = new Vector3(-2, 0);
//             } else {
//                 this.transform.position = new Vector3(2, 0);
//             }
//             SpawnObject();
//             SetSkills();
//             SetGear();
//         }

//         public override void Update() {
//             CheckInput();
//         }

//         private void CheckInput() {
//             if(Input.GetMouseButtonDown(0) && character.isPlayer) {
//                 ActiveEquipment[(int)EquipmentSlot.Weapon].Use();
//             }
//         }


//         private void SpawnObject() {
//             if(character.prefab == null) return;

//             this.gameObj = null;
//             Vector3 position = this.transform.position;
//             Quaternion rotation = Quaternion.identity;
//             PointContext context = new PointContext(this, position);
//             gameObj = Instantiate(character.prefab, position, rotation) as GameObject;
//             IContextAware[] components = gameObj.GetComponents<IContextAware>();
//             if (components != null) {
//                 for (int i = 0; i < components.Length; i++) {
//                     components[i].SetContext(context);
//                 }
//             }
//         }

//         private void SetSkills() {
//             if(character.parameters.baseParameters.classList == null) return;
//             var classes = character.parameters.baseParameters.classList.classes;
//             for(int i = 0; i < classes.Count ; i++) {
//                 var c = classes[i];
//                 if (c.abilities.Length < 1) continue;
//                 for(int j = 0; j < c.abilities.Length; j++) {
//                     var skill = c.abilities[j].Create();
//                     var entry = new KeyValuePair<GameClass, Ability>(c.GameClass, skill);
//                     SkillBook.Add(entry);
//                     SkillBook.Last().Value.Caster = this;
//                 }
//             }
//         }

//         private void SetGear() {
//             var equipTable = new InventoryItemCreator[] {
//                 character.equipment.head,
//                 character.equipment.shoulder,
//                 character.equipment.feet,
//                 character.equipment.body,
//                 character.equipment.legs,
//                 character.equipment.neck,
//                 character.equipment.gloves,
//                 character.equipment.waist,
//                 character.equipment.ring1,
//                 character.equipment.ring2,
//                 character.equipment.weapon,
//             };

//             for (int i = 0; i < equipTable.Length; i++) {
//                 if (equipTable[i] != null) {
//                     var item = equipTable[i].Create();
//                     item.isEquipable = true;
//                     itemManager.EquipItem(item, (EquipmentSlot)i);
//                 }
//             }
//         }
//     }
// }
