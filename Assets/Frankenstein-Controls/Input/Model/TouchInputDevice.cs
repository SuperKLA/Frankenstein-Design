using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace Frankenstein.Controls
{
    public class TouchInputDevice : APIModel, IFingerScript
    {
        #region Interface Accessors

        public IFingerScript IFingerScript
        {
            get { return this; }
        }

        #endregion
        
        
        #region APIModel
        
        public override  void Boot(params object[] any)
        {
            this.IFingerScript.Service =  this.SetupServices<IFingerScriptService>();
        }
        
        #endregion


        #region IFingerScript

        IFingerScriptService IAPIEntity<IFingerScriptService, IFingerScriptView>.Service { get; set; }

        IFingerScriptView IAPIEntity<IFingerScriptService, IFingerScriptView>.View { get; set; }

        #endregion
    }
}
