using EntitySystem;

namespace Intelligence {
    public class ActiveQuestContextCreator : PlayerContextCreator {
        private Quest quest;

        public virtual void Setup(Entity entity, Quest quest) {
            this.entity = entity;
            this.quest = quest;
        }

        public override Context GetContext() {
            return new ActivatedQuestContext(quest, entity);
        }
    }
}
