﻿using UnityEngine;

namespace Intelligence {

    public class MoveToLocation : CharacterAction<PointContext> {

        public bool autobreak = true;
        private UnityEngine.AI.NavMeshAgent agent;

        public override void OnStart() {
            Vector3 location = context.point;
            agent = context.entity.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.autoBraking = autobreak;
            agent.SetDestination(location);
        }

        public override CharacterActionStatus OnUpdate() {
            return agent.hasPath && agent.remainingDistance <= 1f ?
                CharacterActionStatus.Completed : CharacterActionStatus.Running;
        }

    }

}