using Frankenstein;
using Frankenstein.Controls.Entities;

namespace Frankenstein.Controls.Views
{
    public class ClickableView : APIViewBehaviour<IClickableService>, IClickableView
    {
        public virtual ClickBubble OnClickIncoming()
        {
            return this.Service.OnClicked(this.gameObject.GetInstanceID());
        }

        public virtual void SetOnOff(bool onOff)
        {
            
        }
    }
}

