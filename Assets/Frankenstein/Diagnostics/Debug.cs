using System;
using UnityEngine;

namespace Frankenstein.Diagnostics
{
    public static class Debug
    {
        public static void DrawBounds(Bounds bounds, Color color, float t = 10)
        {
            Frankenstein.Diagnostics.DrawBounds.DrawDebugBounds(bounds, color, t);
        }
        
        public static void Log(string msg)
        {
            APIDebugConsole.Log(msg);
        }
        
        public static void LogWarning(string msg)
        {
            APIDebugConsole.LogWarning(msg);
        }
        
        public static void LogError(string msg)
        {
            APIDebugConsole.LogError(msg);
        }
        
        public static void LogError(Exception exc)
        {
            APIDebugConsole.LogError(exc.ToString());
        }

        public static void LogErrors(params Exception[] excs)
        {
            for (int c = 0; c < excs.Length; c++)
            {
                APIDebugConsole.LogError(excs[c]);
            }
        }
    }
}