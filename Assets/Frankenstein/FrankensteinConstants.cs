using System;
using UnityEngine;

namespace Frankenstein
{
    public static class FrankensteinConstants
    {
        public static int ClickableLayer
        {
            get { return LayerMask.NameToLayer("Clickable"); }
        }

        public class Constants
        {
            public static float TouchMaxRay = 100f;
        }
    }
}