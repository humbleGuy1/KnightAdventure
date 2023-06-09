﻿using CodeBase.Infrastructure.Services;
using CodeBase.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        public GameObject PlayerGameObject { get; }

        public event Action PlayerCreated;

        public GameObject CreateHud();
        public GameObject CreatePlayer(GameObject initialPoint);
        public void CleanUp();
    }
}