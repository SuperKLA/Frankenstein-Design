using Frankenstein;
using MayorMoon.Controls.SaveGame.Entities;

namespace MayorMoon.Controls.Controller
{
    internal class SaveGamePatcherController : APIController<ISaveGamePatcher>, ISaveGamePatcherService
    {
        #region IAPIDataController

        protected override void OnEntityCreated(ISaveGamePatcher entity)
        {

        }

        #endregion

        
        #region ISaveGamePatcherService

        void ISaveGamePatcherService.Patch(object data)
        {
//            if(this._patches == null)
//                return;
//
//            for (int c = 0; c < this._patches.AllPatches.Count; c++)
//            {
//                var patch = this._patches.AllPatches[c];
//                if (data.Version.Equals(patch.PatchStart))
//                {
//                    var success = patch.Patch(data);
//                    if (success)
//                    {
//                        data.Version = patch.PatchEnd;
//                    }
//                }
//            }
        }

        #endregion
    }
}