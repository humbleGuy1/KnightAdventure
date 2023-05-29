using Codebase.Data;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFatory;

        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFatory)
        {
            _progressService = progressService;
            _gameFatory = gameFatory;
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            foreach(ISavedProgress progressWriter in _gameFatory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
    }
}

