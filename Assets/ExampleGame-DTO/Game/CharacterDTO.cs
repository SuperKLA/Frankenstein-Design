using Frankenstein.DTO;
using UnityEngine;

namespace ExampleGame.DTO
{
    public interface ICharacter
    {
        float MovementSpeed { get; set; }
    }
    
    public class Character : ICharacter
    {
        public virtual float MovementSpeed { get; set; }
    }
    
    public class CharacterDTO : DTOConfig<Character>, ICharacter
    {
        public float _movementSpeed;

        public float MovementSpeed
        {
            get => this._movementSpeed;
            set => this._movementSpeed = value;
        }

        public override Character ToDTO()
        {
            var result = new Character();
            result.MovementSpeed = this.MovementSpeed;
            
            return result;
        }

        [ContextMenu("Setup")]
        public override void Setup()
        {
            
        }
    }
}