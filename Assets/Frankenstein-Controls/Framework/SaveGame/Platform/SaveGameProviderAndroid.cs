using FloatingNutshell.Controls.SaveGame.Entities;

namespace FloatingNutshell.Controls.SaveGame.Controller.Platform
{
    internal class SaveGameProviderAndroid : ISaveGameProviderService
    {
        private ISaveGameSourceProviderService _provider;
        private ISaveGameSerializer _serializer;
        
        private SaveGameProviderAndroid()
        {
        }
        
        public void Dispose()
        {
            
        }


        public static ISaveGameProviderService Create(ISaveGameSourceProviderService provider)
        {
            var result = new SaveGameProviderAndroid();
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