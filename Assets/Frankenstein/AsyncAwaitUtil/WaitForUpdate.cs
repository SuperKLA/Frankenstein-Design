using UnityEngine;

// This can be used as a way to return to the main unity thread when using multiple threads
// with  methods
public class WaitForUpdate : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get { return false; }
    }
}
