using Frankenstein;
using Frankenstein.Controls.Entities;

namespace FloatingNutshell.Controls.SaveGame.Entities
{
    public interface ISaveGameProvider : IAPIEntity<ISaveGameProviderService>
    {
        ISaveGameSourceProviderService SaveGameSource { get; }
    }

    public interface ISaveGameProviderService : IAPIEntityService, ISaveGameProviderQuery
    {
        object Get();
        void Set(object data);
    }
    
    public interface ISaveGameProviderQuery : IQueryService
    {
        bool HasSaveGame { get; }
    }
}