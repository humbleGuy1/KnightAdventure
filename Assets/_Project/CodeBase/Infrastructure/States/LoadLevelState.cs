using CodeBase.CameraLog;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;

        private const string InitialPointTag = "PlayerInitialPoint";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, 
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        } 

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint);
            _gameFactory.CreateHud();

            CameraFollow(player);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject player)
        {
            Camera camera = Camera.main;
            Follower follower = camera.GetComponent<Follower>();
            follower.Follow(player);
        }
    }
}

