using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [Space]
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoverToPlayer _follower;

        private Coroutine _agrCoroutine;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += Enter;
            _triggerObserver.TriggerExit += Exit;
        }

        private void Start() => 
            SwitchFollowOff();

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= Enter;
            _triggerObserver.TriggerExit -= Exit;
        }

        private void Exit(Collider collider)
        {
            _agrCoroutine = StartCoroutine(SwitchFollowOffAfter(_cooldown));
        }

        private void Enter(Collider collider)
        {
            if(_agrCoroutine != null)
            {
                StopCoroutine(_agrCoroutine);
                _agrCoroutine = null;
            }

            SwitchFollowOn();
        }

        private void SwitchFollowOff() => 
            _follower.enabled = false;

        private void SwitchFollowOn() => 
            _follower.enabled = true;

        private IEnumerator SwitchFollowOffAfter(float cooldown)
        {
            WaitForSeconds waitForSeconds = new(cooldown);

            yield return waitForSeconds;

            SwitchFollowOff();
        }
    }
}

