using Codebase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }

}
