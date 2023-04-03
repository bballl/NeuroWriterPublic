using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    class DropController : MonoBehaviour
    {
        private static Dictionary<Type, Pool<DroppedItem>> _itemPrefabs = new();
        private static AudioSource _audioPrefab;

        [SerializeField] private DroppedItem _dropExp; 
        [SerializeField] private DroppedItem _dropHp; 
        [SerializeField] private DroppedItem _dropGold;

        [SerializeField] private int _dropExpCount;

        [SerializeField] private AudioSource _audio;

        private void Start()
        {
            Initialize(_audio);

            AddDroppedItem(_dropExp, _dropExpCount);
            AddDroppedItem(_dropHp);
            AddDroppedItem(_dropGold);
        }

        public void Initialize(AudioSource audio)
        {
            _itemPrefabs.Clear();

            if (audio == null)
                _audioPrefab = Resources.Load<AudioSource>("Prefabs/DropSound");
            else
                _audioPrefab = audio;

            _audioPrefab = Instantiate(_audioPrefab);
        }

        public void AddDroppedItem(DroppedItem item, int count = 3)
        {
            var type = item.GetType();
            Debug.Log($"Prefab with type [{type}] added to DropController");
            if (!_itemPrefabs.ContainsKey(type))
                _itemPrefabs.Add(type, new Pool<DroppedItem>(count, item, true));
            else if (_itemPrefabs[type] == null)
                _itemPrefabs[type] = new Pool<DroppedItem>(count, item, true);
        }

        public static void PlayDropSound(AudioClip clip)
        {
            _audioPrefab.PlayOneShot(clip);
        }

        public static DroppedItem CreateDrop<T>(int value, Vector3 spawnPosition) where T : DroppedItem
        {
            var type = typeof(T);

            if (_itemPrefabs.ContainsKey(type))
            {
                DroppedItem dropped = _itemPrefabs[type].GetObject();
                dropped.Activate(value, spawnPosition);
                return dropped;
            }
            else
            {
                Debug.LogError($"There is no pool for type {type} in DropController");
                return null;
            }
        }
    }
}
