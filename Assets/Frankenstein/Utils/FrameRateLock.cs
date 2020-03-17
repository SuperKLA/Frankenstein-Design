using UnityEngine;

namespace Frankenstein.Utils
{
    public class FrameRateLock : MonoBehaviour
    {
        public int FrameRate = 30;
        
        public void OnValidate()
        {
            Debug.Log("Locking Frame Rate");
#if UNITY_EDITOR
            QualitySettings.vSyncCount  = 0; // VSync must be disabled
            Application.targetFrameRate = this.FrameRate;
#endif    
        }
    }
    
    
}