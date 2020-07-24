using System;

namespace API.DTO
{
    [Serializable]
    public class SerializableRange
    {
        public SerializableRange() { }


        public SerializableRange(int @from, int @to)
        {
            this._from = from;
            this._to = to;
        }
        
        public int _from;
        public int _to;
        
        public int From => this._from;
        public int To => this._to;

        public bool Contains(int val)
        {
            return val >= this.From && val <= this.To;
        }

        public int Random()
        {
            return UnityEngine.Random.Range(this.From, this.To + 1);
        }
    }
}