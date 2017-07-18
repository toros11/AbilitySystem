using System.Collections.Generic;
using UnityEngine;
using Intelligence;
using System;

namespace EntitySystem {
    public class Quest : EntitySystemBase {
	    [HideInInspector] [SerializeField]
	    public QuestCreator Creator;

        public Texture2D icon;

        public bool IsActive;
        public List<QuestComponent> components;
        public List<QuestRequirement> requirements;

        public Context context;

        public Quest() {
            IsActive = true;
        }

        public Quest(string id) {
            requirements = new List<QuestRequirement>();
            components = new List<QuestComponent>();
        }

        public QuestComponent AddInventoryQuestComponent<T>() where T : QuestComponent, new() {
            QuestComponent component = new T();
            component.quest = this;
            components.Add(component);

            return component;
        }

        public T GetQuestComponent<T>() where T : QuestComponent {
            Type type = typeof(T);
            for(int i = 0; i < components.Count; i++) {
                if (type == components[i].GetType()) return components[i] as T;
            }
            return null;
        }

        public void RemoveQuestComponent(QuestComponent component) {
            components.Remove(component);
        }
        public void OnActivate() {
            for(int i = 0; i < components.Count; i++) {
                components[i].OnActivate();
            }
        }

        public void OnFinish() {
            for(int i = 0; i < components.Count; i++) {
                components[i].OnFinish();
            }
        }
    }
}
