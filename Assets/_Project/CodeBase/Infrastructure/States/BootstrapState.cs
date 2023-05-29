using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
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
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), 
                _services.Single<IGameFactory>()));
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

