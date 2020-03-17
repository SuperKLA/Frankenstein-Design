using FloatingNutshell.Controls.SaveGame.Entities;

namespace FloatingNutshell.Controls.SaveGame.Controller.Platform
{
    internal class SaveGameProviderEditor : ISaveGameProviderService
    {
        private ISaveGameSourceProviderService _provider;
        private ISaveGameSerializer _serializer;
        
        private SaveGameProviderEditor()
        {
        }
        
        public void Dispose()
        {
            this._provider = null;
        }


        public static ISaveGameProviderService Create(ISaveGameSourceProviderService provider)
        {
            var result = new SaveGameProviderEditor();
            result._provider = provider;
            result._serializer = SaveGameSerializer.Create();
            return result;
        }
        
        
        #region ISaveGameProviderService

        bool ISaveGameProviderQuery.HasSaveGame
        {
            get { return this._provider.HasSource; }
        }

        object ISaveGameProviderService.Get()
        {
            var bytes = this._provider.Get();
            if (bytes.Length == 0)
                return null;
            
            return this._serializer.Deserialize(bytes);
        }

        void ISaveGameProviderService.Set(object data)
        {
            var colonyJSON = this._serializer.Serialize(data);
            if (colonyJSON.Length == 0)
            {
                return;
            }

            this._provider.Set(colonyJSON);
        }

        #endregion
    }
}