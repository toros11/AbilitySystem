using EntitySystem;
using Intelligence;
using UnityEngine;

namespace AITests {
    public class EnemyTest : Entity {
        public DecisionPackageCreator dpc;
        public SinglePointContextCreator cc;
        private DecisionPackage dp;


        public override void Init() {
            dp = dpc.Create();

            DoSomething(dp.decisions[0]);

            foreach (var decision in dp.decisions) {
                var contexts = decision.contextCollector.Collect(decision.action, this);
                decision.action.Setup(contexts[0]);
            }

            foreach (var decision in dp.decisions) {
                decision.action.OnStart();
            }
        }

        public override void Update() {
            base.Update();
            ProcessTest();
        }

        void ProcessTest() {
            foreach (var decision in dp.decisions) {
                decision.action.OnUpdate();
            }
        }

        void ProcessDecisions() {
            foreach (var decision in dp.decisions) {
                //decision.evaluator.Score(decision.contextCollector.)
            }

            dp.decisions[0].action.OnUpdate();
        }

        //study more the collector and how to use the decision

        void DoSomething(Decision d) {
            //d.contextCollector.Collect()
        }
    }
}
