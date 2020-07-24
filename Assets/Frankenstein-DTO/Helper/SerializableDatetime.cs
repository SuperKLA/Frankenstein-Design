using System;
using UnityEngine;

namespace API.DTO
{
// we have to use UDateTime instead of DateTime on our classes
// we still typically need to either cast this to a DateTime or read the DateTime field directly
    [Serializable]
    public class SerializableDatetime : ISerializationCallbackReceiver
    {
        [HideInInspector]
        public DateTime dateTime;

        // if you don't want to use the PropertyDrawer then remove HideInInspector here
        [HideInInspector]
        [SerializeField]
        private string _dateTime;

        public static implicit operator DateTime(SerializableDatetime udt)
        {
            return (udt.dateTime);
        }

        public static implicit operator SerializableDatetime(DateTime dt)
        {
            return new SerializableDatetime() {dateTime = dt};
        }

        public void OnAfterDeserialize()
        {
            DateTime.TryParse(_dateTime, out dateTime);
        }

        public void OnBeforeSerialize()
        {
            _dateTime = dateTime.ToString();
        }
    }
}