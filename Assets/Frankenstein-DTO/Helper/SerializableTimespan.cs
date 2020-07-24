using System;
using UnityEngine;

namespace API.DTO
{
    [Serializable]
    public class SerializableTimespan : ISerializationCallbackReceiver
    {
        [HideInInspector]
        public TimeSpan timeSpan;

        // if you don't want to use the PropertyDrawer then remove HideInInspector here
        [HideInInspector]
        [SerializeField]
        private string _timeSpan;

        public static implicit operator TimeSpan(SerializableTimespan udt)
        {
            if (udt == null) return new TimeSpan();
            return (udt.timeSpan);
        }

        public static implicit operator SerializableTimespan(TimeSpan dt)
        {
            return new SerializableTimespan() {timeSpan = dt};
        }

        public void OnAfterDeserialize()
        {
            TimeSpan.TryParse(this._timeSpan, out this.timeSpan);
        }

        public void OnBeforeSerialize()
        {
            this._timeSpan = this.timeSpan.ToString();
        }
    }
}