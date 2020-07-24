using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public enum ClickBubble
    {
        continueBubble = 0,
        abort = 1
    }
    
    public interface IClickableService : IAPIEntityService
    {
        ClickBubble OnClicked(int instanceID);
        void SwitchOnOff(bool onOff);
    }

    public interface IClickableView : IAPIEntityView
    {
        void SetOnOff(bool onOff);
    }
}