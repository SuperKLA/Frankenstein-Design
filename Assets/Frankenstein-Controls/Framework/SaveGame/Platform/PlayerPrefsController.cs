using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;

namespace API.Utils
{
    public interface IPlayerPrefs
    {
        /// <summary>
        /// Has given Player Pref
        /// </summary>
        /// <returns></returns>
        bool Has();

        /// <summary>
        /// Get PlayerPref Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        System.Object Get<T>();
        
        
        bool Set<T>(T value);
    }
    
    public class PlayerPrefsController : IPlayerPrefs
    {
        private string _key;

        private PlayerPrefsController()
        {
        }
        
        public static IPlayerPrefs Create(string key)
        {
            var result = new PlayerPrefsController();
            result._key = key;
            return result;
        }


        #region IPlayerPrefs

        bool IPlayerPrefs.Has()
        {
            return PlayerPrefs.HasKey(this._key);
        }

        System.Object IPlayerPrefs.Get<T>()
        {
            if (typeof(T) == typeof(float))
            {
                return PlayerPrefs.GetFloat(this._key);
            }

            if (typeof(T) == typeof(int))
            {
                return PlayerPrefs.GetInt(this._key);
            }
            if (typeof(T) == typeof(string))
            {
                return PlayerPrefs.GetString(this._key);
            }

            return 0;
        }

        bool IPlayerPrefs.Set<T>(T value)
        {
            Profiler.BeginSample("IPlayerPrefs->Set");
            try
            {
                if (typeof(T) == typeof(float))
                {
                    PlayerPrefs.SetFloat(this._key, Convert.ToSingle(value));
                }

                else if (typeof(T) == typeof(int))
                {
                    PlayerPrefs.SetInt(this._key, Convert.ToInt32(value));
                }
                else if (typeof(T) == typeof(string))
                {
                    PlayerPrefs.SetString(this._key, Convert.ToString(value));
                }

                PlayerPrefs.Save();
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Profiler.EndSample();
            }
        }
        
        #endregion
    }
}