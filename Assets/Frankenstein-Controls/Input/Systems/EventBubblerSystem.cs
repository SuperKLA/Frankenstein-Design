using Frankenstein.Controls.Components;
using Frankenstein.Groups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Frankenstein.Controls.Systems
{
    [UpdateInGroup(typeof(PhysicsGroup))]
    public class EventBubblerSystem : ComponentSystem
    {
        public NativeQueue<EventBubbleData> EventQueue;

        protected override void OnCreate()
        {
            this.EventQueue = new NativeQueue<EventBubbleData>(Allocator.Persistent);
            base.OnCreate();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.EventQueue.Dispose();    
        }

        protected override void OnUpdate()
        {
            if (this.EventQueue.Count == 0) return;
            
            var data = new EventBubbleData();
            //TODO Dequeue sollte ein abort erlauben
            if (!this.EventQueue.TryDequeue(out data)) return;
            {
                var entity = data.EventEntity;

                if (!this.EntityManager.HasComponent<OnClickEventData>(entity)) return;

                this.EntityManager.SetComponentData(entity, new OnClickEventData()
                {
                    IsClicked = true
                });
            }
        }
    }
}