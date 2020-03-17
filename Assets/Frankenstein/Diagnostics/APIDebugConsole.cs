using System;


namespace Frankenstein.Diagnostics
{
    internal static class APIDebugConsole
    {
        internal static void Log(string line)
        {
            UnityEngine.Debug.Log(line);
            // Do further stuff
        }
        
        internal static void LogWarning(string line)
        {
            UnityEngine.Debug.LogWarning(line);
            // Do further stuff
        }
        
        internal static void LogError(string line)
        {
            UnityEngine.Debug.LogError(line);
            // Do further stuff
        }

        internal static void LogError(Exception exc)
        {
            // Do further stuff
            UnityEngine.Debug.LogException(exc);
        }
    }
}