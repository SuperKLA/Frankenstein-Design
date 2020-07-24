using Frankenstein.Utils;
using UnityEngine;

public class PlayerPrefsKill : MonoBehaviour
{
    public string Son;
    public string Father;
    public string GrandFather;
    
    [ContextMenu("KillPlayerPrefs")]
    public void KillPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs.DeleteAll Done");
    }
    
    [ContextMenu("KillSaveFile"), EasyButton]
    public void KillSaveFile()
    {
        var fileWriter = FileWriter.Create(Son);
        var success = fileWriter.Delete();
        
        var fileWriter2 = FileWriter.Create(Father);
        var success2    = fileWriter2.Delete();
        
        var fileWriter3 = FileWriter.Create(GrandFather);
        var success3    = fileWriter3.Delete();
        Debug.Log("PlayerPrefs.KillSaveFile "+(success && success2 && success3));
    }
}