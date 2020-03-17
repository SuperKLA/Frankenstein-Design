using System.Threading.Tasks;
using FloatingNutshell.Controls.SaveGame.Entities;
using Frankenstein;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    internal class SaveGameSourceProvider : APIController<ISaveGameSourceProvider>, ISaveGameSourceProviderService
    {
        private ISaveGameSourceProviderService saveGameProvider;
        
        #region IAPIDataController
        protected override void OnEntityCreated(ISaveGameSourceProvider entity)
        {
            if (!entity.UseSocialCloudData)
            {
                //saveGameProvider.Service = SaveGameSourcePlayerPrefs.Create(entity);
                saveGameProvider = SaveGameSourceFile.Create(entity);
            }
            else
            {
                // TODO if cloud is enabled use one of the specified platforms
//            
//#if UNITY_EDITOR || UNITY_STANDALONE
//            saveGameProvider.Service = SaveGameSourcePlayerPrefs.Create(entity);
//#elif UNITY_IOS
//            saveGameProvider.Service = SaveGameSourcePlayerPrefs.Create(entity);
//#elif UNITY_ANDROID
//            saveGameProvider.Service = SaveGameSourcePlayerPrefs.Create(entity);
//#endif
            }
        }

        public void Dispose()
        {
            
        }
        #endregion


        #region ISaveGameSourceProviderService

        bool ISaveGameSourceProviderService.SourceSuccess
        {
            get { return saveGameProvider.SourceSuccess; }
        }

        bool ISaveGameSourceProviderService.HasSource
        {
            get { return saveGameProvider.HasSource; }
        }

        byte[] ISaveGameSourceProviderService.Get()
        {
            return saveGameProvider.Get();
        }

        bool ISaveGameSourceProviderService.Set(byte[] value)
        {
            return saveGameProvider.Set(value);
        }
        #endregion
    }
}
