using System;
using System.IO;
using System.Threading;
using FloatingNutshell.Controls.SaveGame.Entities;
using Frankenstein.Utils;
using UnityEngine;
using Debug = Frankenstein.Diagnostics.Debug;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    public interface ISaveGameBackUp
    {
        byte[] Get();

        byte[] GetSon();
        byte[] GetFather();
        byte[] GetGrandFather();

        bool Set(byte[] value);

        bool IsSonValid();
        bool IsFatherValid();
        bool IsGrandFatherValid();

        bool IsValid();
    }

    internal class SaveGameBackUp : ISaveGameBackUp
    {
        private IFileWriter _grandFatherFileWriter;
        private IFileWriter _fatherFileWriter;
        private IFileWriter _sonFileWriter;

        private string SonFileName         { get; set; }
        private string FatherFileName      { get; set; }
        private string GrandFatherFileName { get; set; }

        private TimeSpan CreationTimeSpan      { get; set; }
        private TimeSpan MinRunTime            { get; set; }
        private TimeSpan TimeToSaveFather      { get; set; }
        private TimeSpan TimeToSaveGrandFather { get; set; }

        private SaveGameBackUp()
        {
        }

        public void Dispose()
        {
        }


        public static ISaveGameBackUp Create(ISaveGameSourceProvider entity)
        {
            var result = new SaveGameBackUp();

            result.SonFileName         = entity.SonFileName;
            result.FatherFileName      = entity.FatherFileName;
            result.GrandFatherFileName = entity.GrandFatherFileName;

            result.CreationTimeSpan      = entity.CreationTimeSpan;
            result.MinRunTime            = entity.MinRunTime;
            result.TimeToSaveFather      = entity.TimeToSaveFather;
            result.TimeToSaveGrandFather = entity.TimeToSaveGrandFather;

            result._sonFileWriter         = FileWriter.Create(entity.SonFileName);
            result._fatherFileWriter      = FileWriter.Create(entity.FatherFileName);
            result._grandFatherFileWriter = FileWriter.Create(entity.GrandFatherFileName);
            return result;
        }


        #region ISaveGameSourceProviderService

        byte[] ISaveGameBackUp.GetSon()
        {
            return this._sonFileWriter.Read();
        }

        byte[] ISaveGameBackUp.GetFather()
        {
            return this._fatherFileWriter.Read();
        }

        byte[] ISaveGameBackUp.GetGrandFather()
        {
            return this._grandFatherFileWriter.Read();
        }

        bool ISaveGameBackUp.Set(byte[] value)
        {
            var path = _GetPathBasedOnOS();

#if UNITY_EDITOR
            Debug.Log("Writing File " + path);
#endif

            new Thread((para) =>
            {
                var container      = para as RefContainer<string, float, byte[]>;
                var data           = container.Val3;
                var timeSinceStart = container.Val2;
                var altPath        = container.Val1;

                var son    = FileWriter.Create(SonFileName, altPath);
                var father = FileWriter.Create(FatherFileName, altPath);
                var grand  = FileWriter.Create(GrandFatherFileName, altPath);

                son.Write(data, altPath);

                if (this._CanSaveFather(father, altPath, timeSinceStart))
                {
                    father.Write(data, altPath);
                }

                if (this._CanSaveGrandFather(grand, altPath, timeSinceStart))
                {
                    grand.Write(data, altPath);
                }
            }).Start(new RefContainer<string, float, byte[]>
            {
                Val1 = path,
                Val2 = Time.realtimeSinceStartup,
                Val3 = value
            });

            return true;
        }

        private string _GetPathBasedOnOS()
        {
            if (Application.isEditor)
                return Application.persistentDataPath + "/";
            else if (Application.platform == RuntimePlatform.WebGLPlayer) //this on is untested
                return Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/";
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
                return Application.persistentDataPath;
            else // For standalone player.
                return Application.persistentDataPath + "/";
        }

        private bool _CanSaveFather(IFileWriter writer, string altPath = "", float altTime = 0f)
        {
            var fatherTime = TimeToSaveFather;

            var timeDiff         = DateTime.UtcNow - writer.GetLastWriteTimeUTC(altPath);
            var timeCreationDiff = DateTime.UtcNow - writer.GetCreationTimeUTC(altPath);

            if (this._IsInCreationTime(timeCreationDiff.TotalSeconds)) //to write just after creation some values
                return true;

            return this._HasMinRunTime(altTime) && timeDiff.TotalSeconds > fatherTime.TotalSeconds;
        }

        private bool _CanSaveGrandFather(IFileWriter writer, string altPath = "", float altTime = 0f)
        {
            var grandfatherTime  = TimeToSaveGrandFather;
            var timeDiff         = DateTime.UtcNow - writer.GetLastWriteTimeUTC(altPath);
            var timeCreationDiff = DateTime.UtcNow - writer.GetCreationTimeUTC(altPath);

            if (this._IsInCreationTime(timeCreationDiff.TotalSeconds)) //to write just after creation some values
                return true;

            return this._HasMinRunTime(altTime) && timeDiff.TotalSeconds > grandfatherTime.TotalSeconds;
        }

        private bool _HasMinRunTime(float altTime = 0f)
        {
            var timePast   = altTime == 0f ? Time.realtimeSinceStartup : altTime;
            var minRunTime = MinRunTime;
            return timePast > minRunTime.TotalSeconds;
        }

        private bool _IsInCreationTime(double secs)
        {
            var minRunTime = CreationTimeSpan;
            return secs < minRunTime.TotalSeconds;
        }

        bool ISaveGameBackUp.IsValid()
        {
            return this._sonFileWriter.IsValid || this._fatherFileWriter.IsValid || this._grandFatherFileWriter.IsValid;
        }

        bool ISaveGameBackUp.IsSonValid()
        {
            return this._sonFileWriter.IsValid;
        }

        bool ISaveGameBackUp.IsFatherValid()
        {
            return this._fatherFileWriter.IsValid;
        }

        bool ISaveGameBackUp.IsGrandFatherValid()
        {
            return this._grandFatherFileWriter.IsValid;
        }

        byte[] ISaveGameBackUp.Get()
        {
            if (this._sonFileWriter.IsValid)
                return this._sonFileWriter.Read();

            if (this._fatherFileWriter.IsValid)
                return this._fatherFileWriter.Read();

            if (this._grandFatherFileWriter.IsValid)
                return this._grandFatherFileWriter.Read();

            return new byte[0];
        }

        #endregion
    }
}