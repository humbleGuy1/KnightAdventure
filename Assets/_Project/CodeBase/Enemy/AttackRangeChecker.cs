using UnityEngine;

namespace CodeBase.Enemy
{
    public class AttackRangeChecker : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += Enter;
            _triggerObserver.TriggerExit += Exit;
        }

        private void Start()
        {
            _attack.DisableAttack();
        }

        private void Exit(Collider obj)
        {
            _attack.DisableAttack();

        }

        private void Enter(Collider obj)
        {
            _attack.EnableAttack();
        }
    }
}

