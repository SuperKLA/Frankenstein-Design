using API.Utils;
using FloatingNutshell.Controls.SaveGame.Entities;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    internal class SaveGameSourcePlayerPrefs : ISaveGameSourceProviderService
    {
        private IPlayerPrefs _saveGamePrefs;
        
        
        private SaveGameSourcePlayerPrefs()
        {           
        }
        
        public void Dispose()
        {
            this._saveGamePrefs = null;
        }
        
        
        public static ISaveGameSourceProviderService Create(ISaveGameSourceProvider entity)
        {
            var result = new SaveGameSourcePlayerPrefs();
            result._saveGamePrefs = PlayerPrefsController.Create(entity.SaveGameSourceKey);
            return result;
        }

        
        #region ISaveGameSourceProviderService

        bool ISaveGameSourceProviderService.SourceSuccess
        {
            get { return true; }
        }

        bool ISaveGameSourceProviderService.HasSource
        {
            get { return this._saveGamePrefs.Has(); }
        }

        byte[] ISaveGameSourceProviderService.Get()
        {
            return null;
        }

        bool ISaveGameSourceProviderService.Set(byte[] value)
        {
            return false;
        }

        #endregion
    }
}
