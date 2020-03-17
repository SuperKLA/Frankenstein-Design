using System;

namespace API.DTO
{
    [Serializable]
    public struct SerializableVersion
    {
        public int Major;
        public int Minor;
        public int Revision;

        public SerializableVersion(int major, int minor, int revision)
        {
            this.Major    = major;
            this.Minor    = minor;
            this.Revision = revision;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}]", this.Major, this.Minor, this.Revision);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SerializableVersion))
                return false;

            var version = (SerializableVersion) obj;

            var exact = version.Major == this.Major;
            exact = exact && version.Minor    == this.Minor;
            exact = exact && version.Revision == this.Revision;

            return exact;
        }
    }
}