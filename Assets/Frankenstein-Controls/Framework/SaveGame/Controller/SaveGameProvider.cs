using FloatingNutshell.Controls.SaveGame.Controller.Platform;
using FloatingNutshell.Controls.SaveGame.Entities;
using Frankenstein;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    internal class SaveGameProvider : APIController<ISaveGameProvider>, ISaveGameProviderService
    {
        private ISaveGameProviderService saveGameProvider;

        #region IAPIDataController

        protected override void OnEntityCreated(ISaveGameProvider entity)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            saveGameProvider = SaveGameProviderEditor.Create(entity.SaveGameSource);
#elif UNITY_IOS
            saveGameProvider = SaveGameProviderIOS.Create(entity.SaveGameSource);
#elif UNITY_ANDROID
            saveGameProvider = SaveGameProviderAndroid.Create(entity.SaveGameSource);
#endif
        }

        #endregion


        #region ISaveGameProviderService

        bool ISaveGameProviderQuery.HasSaveGame
        {
            get { return saveGameProvider.HasSaveGame; }
        }

        object ISaveGameProviderService.Get()
        {
            return saveGameProvider.Get();
        }

        void ISaveGameProviderService.Set(object data)
        {
            saveGameProvider.Set(data);
        }

        #endregion
    }
}