using System.Collections.Generic;
using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IMeshClickable : IAPIEntity<IMeshClickableService, IMeshClickableView>
    {
        ClickBubble OnClicked(ClickData data);
        
        IList<GameObject> Container  { get; }
        IMainCameraQuery  MainCamera { get; }
    }

    public interface IMeshClickableService : IClickableService
    {
        void Rebind();
    }

    public interface IMeshClickableView : IClickableView
    {
        
    }
}