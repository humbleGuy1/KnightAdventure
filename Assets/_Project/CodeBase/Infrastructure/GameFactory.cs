using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Prefabs/Player";
        private const string HudPath = "Prefabs/HudCanvas";

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            return Instantiate(PlayerPath, initialPoint.transform.position);
        }

        public void CreateHud()
        {
            Instantiate(HudPath);
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
    }
}

