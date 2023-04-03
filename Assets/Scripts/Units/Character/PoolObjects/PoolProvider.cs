using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Character.PoolObjects
{
    internal sealed class PoolProvider
    {
        private readonly Dictionary<string, ObjectPool> providerCache
        = new Dictionary<string, ObjectPool>();

        /// <summary>
        /// Если нет нужного пула объектов, создает его. Запускает метод Pop в пуле.
        /// </summary>
        public GameObject Create(GameObject prefab)
        {
            if (!providerCache.TryGetValue(prefab.name, out ObjectPool viewPool)) //проверяем, есть ли в словаре префаб с таким именем
            {
                viewPool = new ObjectPool(prefab);
                providerCache[prefab.name] = viewPool; //добавляем элемент в словарь
            }

            return viewPool.Pop();
        }

        /// <summary>
        /// Инициирует перемещение объекта в пул.
        /// </summary>
        public void Destroy(GameObject prefab)
        {
            providerCache[prefab.name].Push(prefab);
        }
    }
}
