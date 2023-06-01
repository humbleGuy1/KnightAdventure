using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoverToPlayer _follower;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += Enter;
            _triggerObserver.TriggerExit += Exit;
        }

        private void Start() => 
            _follower.enabled = false;

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= Enter;
            _triggerObserver.TriggerExit -= Exit;
        }

        private void Exit(Collider obj) => 
            _follower.enabled = false;

        private void Enter(Collider obj) => 
            _follower.enabled = true;
    }
}

