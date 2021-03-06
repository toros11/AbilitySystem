using UnityEngine;
using System.Collections.Generic;
using Intelligence;
using System;


namespace EntitySystem {
    public class CharacterEquipment
    {
        public InventoryItemCreator head;
        public InventoryItemCreator shoulder;
        public InventoryItemCreator feet;
        public InventoryItemCreator body;
        public InventoryItemCreator legs;
        public InventoryItemCreator neck;
        public InventoryItemCreator gloves;
        public InventoryItemCreator waist;
        public InventoryItemCreator ring1;
        public InventoryItemCreator ring2;
        public InventoryItemCreator weapon;
    }

    public class CharacterParameters
    {
        public BaseParameters baseParameters;

        [NonSerialized] public IntRange armor;
        [NonSerialized] public IntRange attackBonus;
        [NonSerialized] public IntRange critChance;
        [NonSerialized] public IntRange critMultipiler;
        [NonSerialized] public IntRange counterAttack;
        [NonSerialized] public IntRange casterLevel;
        [NonSerialized] public IntRange meleeLevel;

        [NonSerialized] public IntRange physicalSave;
        [NonSerialized] public IntRange magicalSave;
        [NonSerialized] public IntRange athleticSave;
    }
    public class ClassList {
        public List<PlayableClass> classes;
        public ClassList() {
            classes = new List<PlayableClass>();
        }
    }

    [System.Serializable]
    public class BaseParameters
    {
        public ClassList classList;

        public IntRange strength;
        public IntRange agility;
        public IntRange constitution;
        public IntRange intelligence;
        public IntRange wisdom;
        public IntRange luck;
    }

    public partial class Character : EntitySystemBase {
        public Sprite icon;
        public bool isPlayer;
        public GameObject prefab;

        [SerializeField] public CharacterParameters parameters;
        [SerializeField] public CharacterEquipment equipment;

        [SerializeField] public List <InventoryItemCreator> items;
        [NonSerialized] public List <KeyValuePair<GameClass, Ability>> skillBook;

        public List<CharacterRequirement> requirements;
        public List<CharacterComponent> components;

        [NonSerialized] private Context context;

        public Type contextType;

        public Character() : this ("") {
            this.contextType = contextType ?? typeof(Context);
        }

        public Character(string id) {
            Id = id;
            components = new List<CharacterComponent>();
            requirements = new List<CharacterRequirement>();
            skillBook = new List<KeyValuePair<GameClass, Ability>>();
        }


        public Context GetContext() {
            return context;
        }
    }
}
