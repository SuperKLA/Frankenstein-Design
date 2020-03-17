using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FloatingNutshell.Controls.SaveGame.Entities;
using UnityEngine;
using UnityEngine.Profiling;

namespace FloatingNutshell.Controls.SaveGame.Controller
{
    public interface ISaveGameSerializer
    {
        byte[] Serialize(object data);
        object Deserialize(byte[] data);
    }

    internal class SaveGameSerializer : ISaveGameSerializer
    {
        private ISaveGameSourceProviderService _provider;

        private SaveGameSerializer()
        {
        }

        public void Dispose()
        {
        }

        public static ISaveGameSerializer Create()
        {
            var result = new SaveGameSerializer();
            return result;
        }


        #region ISaveGameSerializer

        byte[] ISaveGameSerializer.Serialize(object data)
        {
            try
            {
                Profiler.BeginSample("ISaveGameSerializer.Serialize:BinaryFormatter");
                {
                    var formatter = new BinaryFormatter();
                    using (var stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, data);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Profiler.EndSample();
            }

            return new byte[0];
        }

        object ISaveGameSerializer.Deserialize(byte[] data)
        {
            try
            {
                Profiler.BeginSample("ISaveGameSerializer.Deserialize:BinaryFormatter");
                {
                    var formatter = new BinaryFormatter();
                    using (var stream = new MemoryStream(data))
                    {
                        return formatter.Deserialize(stream) as object;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Profiler.EndSample();
            }

            return null;
        }

        private void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }

        #endregion
    }
}