using System;
using System.Collections.Generic;

namespace Devdog.General.ThirdParty.FullSerializer {
    partial class fsConverterRegistrar {
        public static Speedup.UnityEngine_Vector2_DirectConverter Register_UnityEngine_Vector2;
    }
}

namespace Devdog.General.ThirdParty.FullSerializer.Speedup {
    public class UnityEngine_Vector2_DirectConverter : fsDirectConverter<UnityEngine.Vector2> {
        protected override fsResult DoSerialize(UnityEngine.Vector2 model, Dictionary<string, fsData> serialized) {
            var result = fsResult.Success;

            result += SerializeMember(serialized, null, "x", model.x);
            result += SerializeMember(serialized, null, "y", model.y);

            return result;
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref UnityEngine.Vector2 model) {
            var result = fsResult.Success;

            var t0 = model.x;
            result += DeserializeMember(data, null, "x", out t0);
            model.x = t0;

            var t1 = model.y;
            result += DeserializeMember(data, null, "y", out t1);
            model.y = t1;

            return result;
        }

        public override object CreateInstance(fsData data, Type storageType) {
            return new UnityEngine.Vector2();
        }
    }
}
