using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory
    {
        public void CreateHud();
        public GameObject CreatePlayer(GameObject initialPoint);
    }
}