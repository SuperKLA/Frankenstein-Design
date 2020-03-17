using System;
using System.IO;
using UnityEngine;

namespace Frankenstein.Utils
{
    public interface IFileWriter
    {
        bool IsValid { get; }
        byte[] Read();
        bool Write(byte[] bytes, string altPath = "");
        bool Delete();
        DateTime GetLastWriteTimeUTC(string altPath = "");
        DateTime GetCreationTimeUTC(string altPath = "");
    }

    public class FileWriter : IFileWriter
    {
        private string _fileName;

        public bool IsValid { get; set; }

        private FileWriter()
        {
        }

        public static IFileWriter Create(string fileName, string altPath = "")
        {
            var result = new FileWriter();
            result._fileName = fileName;

            result._Setup(altPath);

            return result;
        }

        private void _Setup(string altPath = "")
        {
            this.IsValid = false;

            try
            {
                var path     = altPath.Length == 0 ? this._GetPathBasedOnOS() : altPath;
                var filePath = Path.Combine(path, this._fileName);

                if (File.Exists(filePath))
                {
                    this.IsValid = true;
                }
                else
                {
                    this.IsValid = this._Ensure(filePath);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private bool _Ensure(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (var stream = File.Create(filePath))
                {
                }
            }

            return File.Exists(filePath);
        }

        private string _GetPathBasedOnOS()
        {
            if (Application.isEditor)
                return Application.persistentDataPath + "/";
            else if (Application.platform == RuntimePlatform.WebGLPlayer)//this on is untested
                return Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/";
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
                return Application.persistentDataPath;
            else // For standalone player.
                return Application.persistentDataPath + "/";
        }


        public bool Write(byte[] bytes, string altPath = "")
        {
            if (!this.IsValid) return false;

            try
            {
                var path     = altPath.Length == 0 ? this._GetPathBasedOnOS() : altPath;
                var filePath = Path.Combine(path, this._fileName);
                
                File.WriteAllBytes(filePath, bytes);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return false;
        }

        public byte[] Read()
        {
            if (!this.IsValid) return new byte[0];

            try
            {
                var path     = this._GetPathBasedOnOS();
                var filePath = Path.Combine(path, this._fileName);

                return File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return new byte[0];
        }
        
        public bool Delete()
        {
            if (!this.IsValid) return false;

            try
            {
                var path     = this._GetPathBasedOnOS();
                var filePath = Path.Combine(path, this._fileName);

                File.Delete(filePath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return false;
        }

        public DateTime GetLastWriteTimeUTC(string altPath = "")
        {
            if (!this.IsValid) return DateTime.UtcNow;
            
            try
            {
                var path     = altPath.Length == 0 ? this._GetPathBasedOnOS() : altPath;
                var filePath = Path.Combine(path, this._fileName);

                return File.GetLastWriteTimeUtc(filePath);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return DateTime.UtcNow;
        }
        
        public DateTime GetCreationTimeUTC(string altPath = "")
        {
            if (!this.IsValid) return DateTime.UtcNow;
            
            try
            {
                var path     = altPath.Length == 0 ? this._GetPathBasedOnOS() : altPath;
                var filePath = Path.Combine(path, this._fileName);

                return File.GetCreationTimeUtc(filePath);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return DateTime.UtcNow;
        }
    }
}