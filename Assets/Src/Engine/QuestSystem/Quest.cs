using System.Collections.Generic;
using UnityEngine;
using Intelligence;

namespace EntitySystem {
    public class Quest : EntitySystemBase {
	    [HideInInspector] [SerializeField]
	    public StatusEffectCreator Creator;

        public Texture2D icon;

        public bool IsActive;
        public List<QuestComponent> components;
        public Context context;
        public Quest() {
            IsActive = true;
        }
    }
}