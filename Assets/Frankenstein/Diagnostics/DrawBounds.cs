using System;
using UnityEngine;

namespace Frankenstein.Diagnostics
{
    public static class DrawBounds
    {
        public static void DrawDebugBounds(Bounds bounds, Color color, float t)
        {
            Vector3 v3Center  = bounds.center;
            Vector3 v3Extents = bounds.extents;
  
            var v3FrontTopLeft     = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z); // Front top left corner
            var v3FrontTopRight    = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z); // Front top right corner
            var v3FrontBottomLeft  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z); // Front bottom left corner
            var v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z); // Front bottom right corner
            var v3BackTopLeft      = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z); // Back top left corner
            var v3BackTopRight     = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z); // Back top right corner
            var v3BackBottomLeft   = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z); // Back bottom left corner
            var v3BackBottomRight  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z); 
            
            UnityEngine.Debug.DrawLine (v3FrontTopLeft, v3FrontTopRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontTopRight, v3FrontBottomRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontBottomRight, v3FrontBottomLeft, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontBottomLeft, v3FrontTopLeft, color,t, false);
         
            UnityEngine.Debug.DrawLine (v3BackTopLeft, v3BackTopRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3BackTopRight, v3BackBottomRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3BackBottomRight, v3BackBottomLeft, color,t, false);
            UnityEngine.Debug.DrawLine (v3BackBottomLeft, v3BackTopLeft, color,t, false);
         
            UnityEngine.Debug.DrawLine (v3FrontTopLeft, v3BackTopLeft, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontTopRight, v3BackTopRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontBottomRight, v3BackBottomRight, color,t, false);
            UnityEngine.Debug.DrawLine (v3FrontBottomLeft, v3BackBottomLeft, color,t, false);
        }
    }
}