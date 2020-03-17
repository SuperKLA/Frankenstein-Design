using System;
using System.Globalization;
using UnityEngine;

namespace Frankenstein.Utils
{
    public static class GeneralUtils
    {
        public static void DeleteAllChildren(Transform trans)
        {
            var childCount = trans.childCount;
            for (var c = childCount - 1; c >= 0; c--)
            {
#if UNITY_EDITOR
                GameObject.DestroyImmediate(trans.GetChild(c).gameObject);
#else
                GameObject.Destroy(trans.GetChild(c).gameObject);
#endif
            }
        }

        public static string AppVersionShort()
        {
            var version  = Application.version;
            var verSplit = version.Split('.');
            return verSplit[0] + "_" + verSplit[1];
        }

        public static string DateTimeToJSString(DateTime date)
        {
            //return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            return date.ToString("O");
        }
        
        public static DateTime JSToDateTimeUTC(string date)
        {
            try
            {
                DateTime oResult;
                if (DateTime.TryParseExact(date, "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out oResult))
                {
                    return oResult;
                }
                
                if (date.IndexOf('.') == -1)
                {
                    return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }

                DateTime result;
                if (DateTime.TryParseExact(date, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
                {
                    return result;
                }
                else if (DateTime.TryParseExact(date, "yyyy-MM-ddTHH:mm:ss.ffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
                {
                    return result;
                }
                else if (DateTime.TryParseExact(date, "yyyy-MM-ddTHH:mm:ss.fZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
                {
                    return result;
                }

                return result;
            }
            catch (Exception e)
            {
                return DateTime.MinValue;
            }
        }
    }
}