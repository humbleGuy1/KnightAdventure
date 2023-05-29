using System;

namespace Codebase.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data Position;

        public PositionOnLevel(string intialLevel)
        {
            Level = intialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }
    }
}
