using FloatingNutshell.Controls.SaveGame.Entities;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    internal class SaveGameSourceFile : ISaveGameSourceProviderService
    {
        private ISaveGameBackUp _saveGameBack;
        
        private SaveGameSourceFile()
        {           
        }
        
        public void Dispose()
        {

        }
        
        
        public static ISaveGameSourceProviderService Create(ISaveGameSourceProvider entity)
        {
            var result = new SaveGameSourceFile();
            result._saveGameBack = SaveGameBackUp.Create(entity);
            return result;
        }

        
        #region ISaveGameSourceProviderService

        bool ISaveGameSourceProviderService.SourceSuccess
        {
            get { return true; }
        }

        bool ISaveGameSourceProviderService.HasSource
        {
            get { return this._saveGameBack.IsValid(); }
        }

        byte[] ISaveGameSourceProviderService.Get()
        {
            return this._saveGameBack.Get();
        }

        bool ISaveGameSourceProviderService.Set(byte[] value)
        {
            return this._saveGameBack.Set(value);
        }

        #endregion
    }
}
