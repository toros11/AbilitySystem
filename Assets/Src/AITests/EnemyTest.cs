using EntitySystem;
using Intelligence;
using UnityEngine;

namespace AITests {
    public class EnemyTest : Entity {
        public DecisionPackageCreator dpc;
        private DecisionPackage dp;

        public override void Init() {
            print("init");
            dp = dpc.Create();
            print(dp.decisions[0].name);
            
        }
    }
}
