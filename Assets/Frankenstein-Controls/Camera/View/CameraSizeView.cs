using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class CameraSizeView : APIViewBehaviour<ICameraSizeService>, ICameraSizeView
    {
        public BoxCollider2D ColliderBounds;

        public float GetSize()
        {
            var height = Screen.height;
            var width  = Screen.width;

#if UNITY_EDITOR
            var res  = UnityEditor.UnityStats.screenRes;
            var resStrings = res.Split('x');
            width  = int.Parse(resStrings[0]);
            height = int.Parse(resStrings[1]);
#endif


            var bounds = ColliderBounds.bounds;
            var size   = bounds.size;

            var screenRatio = (float) width / (float) height;
            var targetRatio = size.x        / size.y;

            if (screenRatio >= targetRatio)
            {
                return size.y / 2;
            }
            else
            {
                var differenceInSize = targetRatio / screenRatio;
                return size.y / 2 * differenceInSize;
            }
        }

        [ContextMenu("Test")]
        public void Test()
        {
            var v = this.GetSize();
            Debug.Log(v+"");
        }
    }
}