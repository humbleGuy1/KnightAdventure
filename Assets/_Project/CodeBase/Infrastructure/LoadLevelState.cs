using CodeBase.CameraLog;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        private const string InitialPointTag = "PlayerInitialPoint";
        private const string PlayerPath = "Prefabs/Player";
        private const string HudPath = "Prefabs/HudCanvas";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
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
            var initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);

            GameObject player = Instantiate(PlayerPath, initialPoint.transform.position);
            Instantiate(HudPath);

            CameraFollow(player);

            _stateMachine.Enter<GameLoopState>();
        }

        private GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(prefab);
        } 
        
        private GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        private void CameraFollow(GameObject player)
        {
            Camera camera = Camera.main;
            Follower follower = camera.GetComponent<Follower>();
            follower.Follow(player);
        }
    }
}

