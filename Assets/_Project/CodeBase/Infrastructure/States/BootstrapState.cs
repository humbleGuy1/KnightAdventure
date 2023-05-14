﻿using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Main");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandAloneInputService();
            else
                return new TouchInputService();
        }
    }
}

