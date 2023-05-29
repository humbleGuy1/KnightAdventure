using CodeBase.Infrastructure.Services;
using CodeBase.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public void CreateHud();
        public GameObject CreatePlayer(GameObject initialPoint);
        public void CleanUp();
    }
}