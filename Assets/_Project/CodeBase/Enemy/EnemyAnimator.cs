using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private static readonly int AttackHash = Animator.StringToHash("Attack_1");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _walkingStateHash = Animator.StringToHash("MoveBlendTree");
        private readonly int _deathStateHash = Animator.StringToHash("die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void PlayHit() => _animator.SetTrigger(HitHash);

        public void PlayDeath() => _animator.SetTrigger(DieHash);

        public void Move(float speed)
        {
            _animator.SetBool(IsMovingHash, true);
            _animator.SetFloat(SpeedHash, speed);
        }

        public void StopMoving() => _animator.SetBool(IsMovingHash, false);

        public void PlayAttack() => _animator.SetTrigger(AttackHash);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
          StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}

