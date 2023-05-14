﻿namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        public void Enter(TPayLoad payLoad);
    }
    
    public interface IExitableState
    {
        public void Exit();
    }

}
