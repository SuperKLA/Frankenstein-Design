using Frankenstein.DTO;
using UnityEngine;

namespace ExampleGame.DTO
{
    public interface IGameConfig<T_Character>
    {
        T_Character Character { get; set; }
    }
    
    public class GameConfig : IGameConfig<Character>
    {
        public virtual Character Character { get; set; }
    }
    
    public class GameConfigDTO : DTOConfig<GameConfig>, IGameConfig<CharacterDTO>
    {
        public CharacterDTO _character;

        public CharacterDTO Character
        {
            get => this._character;
            set => this._character = value;
        }

        public override GameConfig ToDTO()
        {
            var result = new GameConfig();
            result.Character = this.Character.ToDTO();

            return result;
        }

        [ContextMenu("Setup")]
        public override void Setup()
        {
            
        }
    }
}