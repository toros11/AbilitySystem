using System;
using System.Collections.Generic;
using Intelligence;

namespace EntitySystem {

    public class QuestManager {
        protected List<Quest> questList;
        protected Entity entity;
        
        public QuestManager(Entity entity) {
            this.entity = entity;
            questList = new List<Quest>();
        }
        public void Update() {

        }
    }
}