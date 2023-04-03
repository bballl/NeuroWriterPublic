using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Character.PoolObjects
{
    internal sealed class ObjectPool
    {
        private readonly Stack<GameObject> stack = new Stack<GameObject>();
        private readonly GameObject prefab;
        private readonly Transform root;

        public ObjectPool(GameObject prefab)
        {
            this.prefab = prefab;
            root = new GameObject($"[{this.prefab.name}]root").transform;

            //Observer.DestroyBulletEvent += Push;
        }

        public void Push(GameObject go)
        {
            stack.Push(go);
            go.transform.SetParent(root);
            go.SetActive(false);
        }

        public GameObject Pop()
        {
            GameObject go;
            if (stack.Count == 0)
            {
                go = UnityEngine.Object.Instantiate(prefab);
                go.name = prefab.name;
            }
            else
            {
                go = stack.Pop();
            }

            go.SetActive(true);
            go.transform.SetParent(null);

            return go;
        }

        public void Dispose()
        {
            //Observer.DestroyBulletEvent -= Push;

            for (var i = 0; i < stack.Count; i++)
            {
                var gameObject = stack.Pop();
                UnityEngine.Object.Destroy(gameObject);
            }
            UnityEngine.Object.Destroy(root.gameObject);
        }
    }
}
