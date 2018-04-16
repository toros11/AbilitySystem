using System;
using System.Collections.Generic;

namespace Devdog.General.ThirdParty.FullSerializer {
    partial class fsConverterRegistrar {
        public static Speedup.Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusChoiceNode_PlayerDecisionsQuestStatus_DirectConverter Register_Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusChoiceNode_PlayerDecisionsQuestStatus;
    }
}

namespace Devdog.General.ThirdParty.FullSerializer.Speedup {
    public class Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusChoiceNode_PlayerDecisionsQuestStatus_DirectConverter : fsDirectConverter<Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusChoiceNode.PlayerDecisionsQuestStatus> {
        protected override fsResult DoSerialize(Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusChoiceNode.PlayerDecisionsQuestStatus model, Dictionary<string, fsData> serialized) {
            var result = fsResult.Success;

            result += SerializeMember(serialized, null, "questStatus", model.questStatus);
            result += SerializeMember(serialized, null, "playerDecisions", model.playerDecisions);

            return result;
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusChoiceNode.PlayerDecisionsQuestStatus model) {
            var result = fsResult.Success;

            var t0 = model.questStatus;
            result += DeserializeMember(data, null, "questStatus", out t0);
            model.questStatus = t0;

            var t1 = model.playerDecisions;
            result += DeserializeMember(data, null, "playerDecisions", out t1);
            model.playerDecisions = t1;

            return result;
        }

        public override object CreateInstance(fsData data, Type storageType) {
            return new Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusChoiceNode.PlayerDecisionsQuestStatus();
        }
    }
}
