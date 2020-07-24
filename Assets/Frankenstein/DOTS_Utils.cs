using Unity.Entities;
using Unity.Physics.Systems;

namespace Frankenstein.Groups
{
    public static class DOTS_Utils
    {
        public static void EnsureEntityHasComponent<T>(EntityManager entityMgr, Entity entity, T data)where T : struct, IComponentData
        {
            if (entityMgr.HasComponent(entity, typeof(T)))
            {
                entityMgr.SetComponentData<T>(entity, data);
            }
            else
            {
                entityMgr.AddComponentData<T>(entity, data);
            }
            
        }
    }
}