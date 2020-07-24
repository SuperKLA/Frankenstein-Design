using System;

namespace API.DTO
{
    [Serializable]
    public struct SerializableGuid : IComparable, IComparable<SerializableGuid>, IEquatable<SerializableGuid>
    {
        public string _value;
        
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public SerializableGuid(string value)
        {
            _value = value;
        }

        public static implicit operator SerializableGuid(Guid guid)
        {
            return new SerializableGuid(guid.ToString());
        }

        public static implicit operator Guid(SerializableGuid serializableGuid)
        {
            if (serializableGuid.Value == null || serializableGuid.Value.Equals(String.Empty))
            {
                return Guid.Empty;
            }

            return new Guid(serializableGuid.Value);
        }
        
        public static implicit operator String(SerializableGuid serializableGuid)
        {
            if (serializableGuid.Value == null || serializableGuid.Value.Equals(String.Empty))
            {
                return "";
            }

            return serializableGuid.Value;
        }
        
        public static implicit operator SerializableGuid(string serializableGuid)
        {
            return new SerializableGuid(serializableGuid);
        }

        public int CompareTo(object value)
        {
            if (value == null)
                return 1;
            if (!(value is SerializableGuid))
                throw new ArgumentException("Must be SerializableGuid");
            SerializableGuid guid = (SerializableGuid) value;
            return guid.Value == Value ? 0 : 1;
        }

        public int CompareTo(SerializableGuid other)
        {
            return other.Value == Value ? 0 : 1;
        }

        public bool Equals(SerializableGuid other)
        {
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return (Value != null ? new Guid(Value).ToString() : string.Empty);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(this.Value) || this.Value.Equals(Guid.Empty+"", StringComparison.OrdinalIgnoreCase);
        }
    }
}