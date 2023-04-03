using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DialogScene")]
public class DialogScene : ScriptableObject
{
    public Sprite BackImage;
    public List<DialogData> Dialogs;
}
