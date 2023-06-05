using System;

namespace Codebase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData { get; private set; }
        public PlayerState PlayerState { get; private set; }

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new PlayerState();
        }
    }
}
