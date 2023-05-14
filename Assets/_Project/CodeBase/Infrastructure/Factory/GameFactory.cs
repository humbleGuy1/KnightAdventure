using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public GameObject CreatePlayer(GameObject initialPoint) => 
            _assetProvider.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateHud() => _assetProvider.Instantiate(AssetPath.HudPath);
    }
}

