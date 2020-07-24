using System.Collections.Generic;
using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public struct ClickData
    {
        public Vector3 Position             { get; set; }
        public Vector2 Screen               { get; set; }
        public int     GameObjectInstanceID { get; set; }
    }

    public interface ISpriteClickable : IAPIEntity<ISpriteClickableService, ISpriteClickableView>
    {
        ClickBubble OnClicked(ClickData data);
        IList<GameObject> SpriteContainer { get; }
        IMainCameraQuery  MainCamera      { get; }
    }

    public interface ISpriteClickableService : IClickableService
    {
        void Rebind();
    }

    public interface ISpriteClickableView : IClickableView
    {
        
    }
}