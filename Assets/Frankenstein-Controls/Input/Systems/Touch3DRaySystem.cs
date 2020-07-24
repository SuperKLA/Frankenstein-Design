using Frankenstein.Controls.Components;
using Frankenstein.Controls.Controller;
using Frankenstein.Groups;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace Frankenstein.Controls.Systems
{
    [UpdateInGroup(typeof(PhysicsGroup))]
    public class Touch3DRaySystem : JobComponentSystem
    {
        private BuildPhysicsWorld            _buildPhysicsWorldSystem;
        public  NativeQueue<EventBubbleData> EventQueue;

        protected override void OnCreate()
        {
            _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
            EventQueue               = World.GetOrCreateSystem<EventBubblerSystem>().EventQueue;

            base.OnCreate();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            if (Input.GetMouseButtonUp(0) || (Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Ended))
            {
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return inputDeps;
                
                inputDeps = JobHandle.CombineDependencies(inputDeps, _buildPhysicsWorldSystem.FinalJobHandle);

                if (MainCameraController.CurrentCamera == null) return inputDeps;
                
                var screenRay = MainCameraController.CurrentCamera.ScreenPointToRay(Input.mousePosition);
                

                Raycast(screenRay.origin, screenRay.GetPoint(110));
                
//                var handle = new FindClosestInteractable
//                {
//                    Input = new RaycastInput
//                    {
//                        Start = screenRay.origin,
//                        End   = screenRay.GetPoint(100),
//                        Filter = new CollisionFilter
//                        {
//                            BelongsTo    = 1u << 3,
//                            CollidesWith = 1u << 3,
//                            GroupIndex   = 0
//                        }
//                    },
//                    EventQueue = EventQueue.AsParallelWriter(),
//                    World      = _buildPhysicsWorldSystem.PhysicsWorld
//                }.Schedule(inputDeps);
//
//                return handle;
            }

            return inputDeps;
        }
        
        public Entity Raycast(float3 RayFrom, float3 RayTo)
        {
            var physicsWorldSystem = Unity.Entities.World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
            var collisionWorld     = physicsWorldSystem.PhysicsWorld.CollisionWorld;
            var input = new RaycastInput()
            {
                Start = RayFrom,
                End   = RayTo,
                Filter = new CollisionFilter()
                {
                    BelongsTo    = 1u << 3,
                    CollidesWith = 1u << 3,
                    GroupIndex   = 0
                }
            };

            var hit     = new RaycastHit();
            var       haveHit = collisionWorld.CastRay(input, out hit);
            
//            if (haveHit)
//            {
//                for (int c = 0; c < hits.Length; c++)
//                {
//                    var hit = hits[c];
//                    var e   = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
//                    EventQueue.Enqueue(new EventBubbleData
//                    {
//                        EventEntity = e,
//                        Hit         = hit,
//                    });
//                }
//            }
            
            if (haveHit)
            {
                // see hit.Position 
                // see hit.SurfaceNormal
                var e = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                EventQueue.Enqueue(new EventBubbleData
                {
                    EventEntity = e,
                    Hit         = hit,
                });
                return e;
            }
            return Entity.Null;
        }

        //[BurstCompile]
        public struct FindClosestInteractable : IJob
        {
            [ReadOnly]
            public PhysicsWorld World;

            [ReadOnly]
            public RaycastInput Input;

            public NativeQueue<EventBubbleData>.ParallelWriter EventQueue;

            public void Execute()
            {
                if (World.CollisionWorld.CastRay(Input, out var hit))
                {
                    Entity entity = World.Bodies[hit.RigidBodyIndex].Entity;
                    EventQueue.Enqueue(new EventBubbleData
                    {
                        EventEntity = entity,
                        Hit         = hit,
                    });
                }
            }
        }
    }
}