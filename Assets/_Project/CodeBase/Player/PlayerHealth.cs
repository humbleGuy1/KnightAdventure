using Codebase.Data;
using CodeBase.Services.PersistentProgress;
using System;
using UnityEngine;


namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        //[SerializeField] private PlayerAnimator
        private PlayerState _playerState;

        public event Action HealthChanged;

        public float CurrentHp
        {
            get => _playerState.CurrentHp;

            set
            {
                if (_playerState.CurrentHp != value)
                {
                    _playerState.SetCurrentHp(value);
                    HealthChanged?.Invoke();
                }
            }
        }

        public float MaxHp
        {
            get => _playerState.MaxHp;
            set => _playerState.SetMaxHp(value);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerState = progress.PlayerState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.SetCurrentHp(CurrentHp);
            progress.PlayerState.SetMaxHp(MaxHp);
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                return;
            }

            CurrentHp -= damage;
            HealthChanged.Invoke();
        }
    }
}

