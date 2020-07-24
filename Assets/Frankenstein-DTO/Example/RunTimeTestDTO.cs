using Unity.Entities;
using UnityEngine;

namespace Frankenstein.DTO
{
    public struct ComponentX : IComponentData
    {
        public float ValVal { get; set; }
    }
    
    public interface IRunTimeTest
    {
        string Value { get; set; }
    }
    
    public class RunTimeTest : IRunTimeTest
    {
        public virtual string Value { get; set; }
    }
    
    [CreateAssetMenu(menuName = "Frankenstein/DTO/RunTimeTestDTO")]
    public class RunTimeTestDTO : DTOObject<RunTimeTest>, IRunTimeTest, IConvertGameObjectToEntity
    {
        public string _value;

        public string Value
        {
            get => this._value;
            set => this._value = value;
        }

        public override RunTimeTest ToDTO()
        {
            var result = new RunTimeTest();
            result.Value = this.Value;
            return result;
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            //fremde compontents von Unity z.b. Translation, wie benutzen?
            dstManager.AddComponent<ComponentX>(entity);
        }

        [ContextMenu("Setup")]
        public override void Setup()
        {
            
        }
    }
}