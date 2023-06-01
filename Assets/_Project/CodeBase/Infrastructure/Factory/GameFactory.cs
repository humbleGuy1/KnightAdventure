using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; private set; } = new List<ISavedProgress>();
        public GameObject PlayerGameObject { get; private set; }

        public event Action PlayerCreated;

        public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            PlayerGameObject = InstantiateRegistered(AssetPath.PlayerPath, initialPoint.transform.position);
            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

        public void CreateHud() => InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, position);

            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);

            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}

