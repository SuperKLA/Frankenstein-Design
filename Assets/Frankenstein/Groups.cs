using Unity.Entities;
using Unity.Physics.Systems;

namespace Frankenstein.Groups
{
    public class PreLogicGroup : ComponentSystemGroup { }
    
    [UpdateAfter(typeof(PreLogicGroup))]
    public class LogicGroup : ComponentSystemGroup { }
    
    [UpdateAfter(typeof(LogicGroup))]
    public class AfterLogicGroup : ComponentSystemGroup { }
    
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class PhysicsGroup : ComponentSystemGroup { }
}