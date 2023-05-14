using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public void CreateHud();
        public GameObject CreatePlayer(GameObject initialPoint);
    }
}