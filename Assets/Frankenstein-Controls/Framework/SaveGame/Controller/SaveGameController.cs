using FloatingNutshell.Controls.SaveGame.Entities;

namespace Frankenstein.Controls.Controller
{
    internal class SaveGameController : APIController<ISaveGame>, ISaveGameService
    {
        private ISaveGameService ISaveGameService
        {
            get { return this; }
        }


        #region IAPIDataController

        protected override void OnEntityCreated(ISaveGame entity)
        {
            this.SaveGame = Setup(entity);
        }

        private object Setup(ISaveGame entity)
        {
            var colonyData = entity.ProviderService.Get();
            entity.GamePatcherService.Patch(colonyData);
            return colonyData;
        }

        #endregion


        #region ISaveGameService

        public object SaveGame { get; set; }

        bool ISaveGameService.HasPlayerData
        {
            get { return this.SaveGame != null; }
        }

        void ISaveGameService.WriteChanges(object writeNew = null)
        {
            if (writeNew != null)
                this.SaveGame = writeNew;

            if (this.SaveGame == null)
                return;
            
            this.Owner.ProviderService.Set(this.SaveGame);
        }

        #endregion


        #region ISaveGameQueryResult

        void ISaveGameQueryResult.SaveNow()
        {
            this.ISaveGameService.WriteChanges();
        }

        #endregion
    }
}