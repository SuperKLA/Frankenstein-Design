using Frankenstein.Controls.Components;
using Frankenstein.Controls.Views;
using Unity.Entities;

namespace Frankenstein.Controls.Systems
{
    public class JoyStickSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // Entities.ForEach processes each set of ComponentData on the main thread. This is not the recommended
            // method for best performance. However, we start with it here to demonstrate the clearer separation
            // between ComponentSystem Update (logic) and ComponentData (data).
            // There is no update logic on the individual ComponentData.

            this.Entities.ForEach((Entity ent, ref JoyStickData data) =>
            {
                var view = EntityManager.GetComponentObject<JoyStickView>(ent);

                data.HorizontalAmount = view.joystick.Horizontal;
                data.VerticalAmount   = view.joystick.Vertical;
            });
        }
    }
}