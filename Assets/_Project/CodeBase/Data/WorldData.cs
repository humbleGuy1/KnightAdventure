using System;

namespace Codebase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string intialLevel) 
        {
            PositionOnLevel = new PositionOnLevel(intialLevel);
        }
    }
}
