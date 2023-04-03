using UnityEngine;

namespace GameBoxProject
{
    [CreateAssetMenu(menuName = "Data/BlockedButton")]
    public class BlockButtonIcons : ScriptableObject
    {
        public IconType IconType;

        public Sprite Icon;
        public string Name;

        public SceneContent ContentPrefab;
    }

    public enum IconType
    {
        Weapon = 0,
        Round = 1
    }
}
