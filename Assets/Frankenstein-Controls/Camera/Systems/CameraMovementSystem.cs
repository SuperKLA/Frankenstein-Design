using Frankenstein.Controls.Components;
using Frankenstein.Controls.Views;
using Unity.Entities;
using Unity.Transforms;

namespace Frankenstein.Controls.Systems
{
    public class CameraMovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // Entities.ForEach processes each set of ComponentData on the main thread. This is not the recommended
            // method for best performance. However, we start with it here to demonstrate the clearer separation
            // between ComponentSystem Update (logic) and ComponentData (data).
            // There is no update logic on the individual ComponentData.

            this.Entities.ForEach((Entity ent, ref CameraMovementData data, ref LocalToWorld translation) =>
            {
                var view = this.EntityManager.GetComponentObject<CameraView>(ent);
                if (view == null) return;
                
                view.OwnTransform.position = translation.Position;
                view.OwnTransform.forward = translation.Forward;
            });
        }
    }
}