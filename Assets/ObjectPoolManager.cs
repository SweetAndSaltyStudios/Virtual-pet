using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class ObjectPoolManager : Singelton<ObjectPoolManager>
    {
        private Dictionary<PrefabAssetType, Stack<GameObject>> pooledObjects = new Dictionary<PrefabAssetType, Stack<GameObject>>();

        public GameObject Spawn(GameObject prefab, Vector2 position = new Vector2(), Quaternion rotation = new Quaternion())
        {
            print(prefab.GetHashCode());

            var newInstance = Instantiate(prefab, position, rotation);
            newInstance.name = prefab.name;
            print(newInstance.GetHashCode());

            Debug.LogWarning(newInstance == prefab);

            return newInstance;
        }

        public void Despawn(GameObject instance)
        {
            Destroy(instance);
        }
    }
}