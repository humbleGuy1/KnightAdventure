﻿namespace CodeBase.Enemy
{
    public interface IAnimationStateReader
    {
        public AnimatorState State { get; }

        public void EnteredState(int stateHash);
        public void ExitedState(int stateHash);

    }
}

