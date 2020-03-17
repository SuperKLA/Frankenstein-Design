using Frankenstein;

namespace MayorMoon.Controls.SaveGame.Entities
{
    public interface ISaveGamePatcher : IAPIEntity<ISaveGamePatcherService>
    {
        
    }

    public interface ISaveGamePatcherService : IAPIEntityService
    {
        void Patch(object data);
    }
}