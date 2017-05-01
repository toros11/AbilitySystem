using UnityEngine;
using UnityEngine.AI;

namespace Intelligence {

    public class EvadeDangerousArea : CharacterAction<PointContext> {
        public float dangerRadius = 10;
        private NavMeshAgent agent;

        public override void OnStart() {
            agent = context.entity.GetComponent<NavMeshAgent>();
        }

        public override CharacterActionStatus OnUpdate() {
            Vector3 point2me = Vector3.Normalize(context.entity.transform.position - context.point);
            Vector3 safePoint = point2me * dangerRadius;

            agent.destination = safePoint;
            return CharacterActionStatus.Running;
        }
    }
}