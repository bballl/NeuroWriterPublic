using UnityEngine;
using UnityEditor;

public class SavedDataCleaner
{
#if UNITY_EDITOR
    [MenuItem("Tools/Clear")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
