using UnityEngine;

[CreateAssetMenu(menuName = "Data/NarrativeText")]
public class Narrative : ScriptableObject
{
    public int Level;
    [Multiline(10)]
    public string Text;
}