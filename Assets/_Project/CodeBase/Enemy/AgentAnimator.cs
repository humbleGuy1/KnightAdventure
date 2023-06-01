using CodeBase.Enemy.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AgentAnimator : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private void Update()
        {
            if (ShouldMove())
                _enemyAnimator.Move(_navMeshAgent.velocity.magnitude);
            else
                _enemyAnimator.StopMoving();
        }

        private bool ShouldMove() =>
            _navMeshAgent.velocity.magnitude > Constants.MinimalVelocity &&
            _navMeshAgent.remainingDistance > _navMeshAgent.radius;
    }
}

