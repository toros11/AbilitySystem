using System;
using System.Collections.Generic;

namespace Devdog.General.ThirdParty.FullSerializer {
    partial class fsConverterRegistrar {
        public static Speedup.Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusDecision_DirectConverter Register_Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusDecision;
    }
}

namespace Devdog.General.ThirdParty.FullSerializer.Speedup {
    public class Devdog_QuestSystemPro_Dialogue_PlayerQuestStatusDecision_DirectConverter : fsDirectConverter<Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusDecision> {
        protected override fsResult DoSerialize(Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusDecision model, Dictionary<string, fsData> serialized) {
            var result = fsResult.Success;

            result += SerializeMember(serialized, null, "action", model.action);
            result += SerializeMember(serialized, null, "option", model.option);

            return result;
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusDecision model) {
            var result = fsResult.Success;

            var t0 = model.action;
            result += DeserializeMember(data, null, "action", out t0);
            model.action = t0;

            var t1 = model.option;
            result += DeserializeMember(data, null, "option", out t1);
            model.option = t1;

            return result;
        }

        public override object CreateInstance(fsData data, Type storageType) {
            return new Devdog.QuestSystemPro.Dialogue.PlayerQuestStatusDecision();
        }
    }
}
