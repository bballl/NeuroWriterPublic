using UnityEngine;

namespace Assets.Scripts.SaveLoadData
{
    internal class SaveData
    {
        public SaveData(string name, int data)
        {
            PlayerPrefs.SetInt(name, data);
        }

        public SaveData(string name, float data)
        {
            PlayerPrefs.SetFloat(name, data);
        }

        public SaveData(string name, string data)
        {
            PlayerPrefs.SetString(name, data);
        }
    }
}
