using CodeBase.CameraLog;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        private const string InitialPointTag = "PlayerInitialPoint";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, 
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        } 

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint);

            _gameFactory.CreateHud();

            CameraFollow(player);
        }

        private void CameraFollow(GameObject player)
        {
            Camera camera = Camera.main;
            Follower follower = camera.GetComponent<Follower>();
            follower.Follow(player);
        }
    }
}

