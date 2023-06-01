using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy.Animation
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private static readonly int AttackHash = Animator.StringToHash("Attack_1");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private static readonly int _idleStateHash = Animator.StringToHash("idle");
        private static readonly int _attackStateHash = Animator.StringToHash("attack01");
        private static readonly int _walkingStateHash = Animator.StringToHash("MoveBlendTree");
        private static readonly int _deathStateHash = Animator.StringToHash("die");

        private readonly Dictionary<int, AnimatorState> _hashStateDictionary = new()
        {
            [_idleStateHash] = AnimatorState.Idle,
            [_attackStateHash] = AnimatorState.Attack,
            [_walkingStateHash] = AnimatorState.Walking,
            [_deathStateHash] = AnimatorState.Died,
        };

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

        public void ExitedState(int stateHash) => StateExited?.Invoke(StateFor(stateHash));


        private AnimatorState StateFor(int stateHash) => _hashStateDictionary[stateHash];
    }
}

