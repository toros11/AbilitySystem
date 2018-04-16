using System;
using System.Collections.Generic;

namespace Devdog.General.ThirdParty.FullSerializer {
    partial class fsConverterRegistrar {
        public static Speedup.Devdog_QuestSystemPro_Dialogue_HasCompletableQuestEdgeCondition_DirectConverter Register_Devdog_QuestSystemPro_Dialogue_HasCompletableQuestEdgeCondition;
    }
}

namespace Devdog.General.ThirdParty.FullSerializer.Speedup {
    public class Devdog_QuestSystemPro_Dialogue_HasCompletableQuestEdgeCondition_DirectConverter : fsDirectConverter<Devdog.QuestSystemPro.Dialogue.HasCompletableQuestEdgeCondition> {
        protected override fsResult DoSerialize(Devdog.QuestSystemPro.Dialogue.HasCompletableQuestEdgeCondition model, Dictionary<string, fsData> serialized) {
            var result = fsResult.Success;

            result += SerializeMember(serialized, null, "quests", model.quests);
            result += SerializeMember(serialized, null, "canViewEndNode", model.canViewEndNode);

            return result;
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Devdog.QuestSystemPro.Dialogue.HasCompletableQuestEdgeCondition model) {
            var result = fsResult.Success;

            var t0 = model.quests;
            result += DeserializeMember(data, null, "quests", out t0);
            model.quests = t0;

            var t1 = model.canViewEndNode;
            result += DeserializeMember(data, null, "canViewEndNode", out t1);
            model.canViewEndNode = t1;

            return result;
        }

        public override object CreateInstance(fsData data, Type storageType) {
            return new Devdog.QuestSystemPro.Dialogue.HasCompletableQuestEdgeCondition();
        }
    }
}
