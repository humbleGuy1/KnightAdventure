using System;

namespace Codebase.Data
{
    [Serializable]
    public class PlayerState
    {
        public float CurrentHp { get; private set; }
        public float MaxHp { get; private set; }

        public void ResetHp() => CurrentHp = MaxHp;

        public void SetMaxHp(float maxHp) => MaxHp = maxHp;

        public void SetCurrentHp(float currentHp) => CurrentHp = currentHp;
    }
}
