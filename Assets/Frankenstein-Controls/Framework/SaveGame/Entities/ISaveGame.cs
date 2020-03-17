using Frankenstein;
using Frankenstein.Controls.Entities;
using MayorMoon.Controls.SaveGame.Entities;

namespace FloatingNutshell.Controls.SaveGame.Entities
{
    public interface ISaveGame : IAPIEntity<ISaveGameService>
    {
        ISaveGameProviderService ProviderService { get; }
        ISaveGamePatcherService GamePatcherService { get; }
    }

    public interface ISaveGameService : IAPIEntityService, ISaveGameQueryResult
    {
        object SaveGame { get; }

        /// <summary>
        /// WriteNew is only used to create the first Save game do not call it afterwards. This will break the Link
        /// </summary>
        /// <param name="writeNew"></param>
        void WriteChanges(object writeNew = null);
        bool HasPlayerData { get; }
    }
    
    public interface ISaveGameQueryResult : IQueryService
    {
        void SaveNow();
    }
}