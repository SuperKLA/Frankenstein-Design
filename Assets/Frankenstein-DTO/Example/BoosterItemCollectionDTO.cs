using System.Collections.Generic;
using UnityEngine;

namespace Frankenstein.DTO
{
    public interface IBoosterItemCollection<T_Booster>
    {
        List<T_Booster> Booster { get; set; }
    }


    public class BoosterItemCollection : IBoosterItemCollection<BoosterItem>
    {
        public virtual List<BoosterItem> Booster { get; set; }
    }
    
    [AddComponentMenu("DTO/Match3/BoosterItemCollectionDTO")]
    public class BoosterItemCollectionDTO : DTOConfig<BoosterItemCollection>, IBoosterItemCollection<BoosterItemDTO>
    {
        public List<BoosterItemDTO> _booster;

        public List<BoosterItemDTO> Booster
        {
            get => this._booster;
            set => this._booster = value;
        }

        public override BoosterItemCollection ToDTO()
        {
            var result = new BoosterItemCollection();
            result.Booster = new List<BoosterItem>();

            for (int c = 0; c < this.Booster.Count; c++)
            {
                var dtoModel = this.Booster[c];
                result.Booster.Add(dtoModel.ToDTO());
            }
            
            return result;
        }

        public void AddBooster()
        {
            var board = new GameObject("Booster" +this.Booster.Count, typeof(BoosterItemDTO));
            board.transform.SetParent(this.transform);

            var boardDTO = board.GetComponent<BoosterItemDTO>();
            boardDTO.Setup();

            this.Booster.Add(boardDTO);
        }
        
        [ContextMenu("Setup")]
        public override void Setup()
        {
            this.Booster = new List<BoosterItemDTO>();
        }
    }
}