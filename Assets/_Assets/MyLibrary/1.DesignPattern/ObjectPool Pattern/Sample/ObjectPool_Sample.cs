using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    [System.Serializable]
    public enum TestObjectEnum
    {
        capsulePrefab,
        cubePrefab,
        cylinderPrefab,
        spherePrefab,
        notInsidePrefab
    }

    public class ObjectPool_Sample : MonoBehaviour
    {
        [Header("NotInside ObjectPool")] [SerializeField]
        private GameObject notInsidePrefab;

        [Header("Select Object")] [SerializeField]
        private TestObjectEnum SelectObject;

        private GameObject GetSelectObject()
        {
            switch (SelectObject)
            {
                case TestObjectEnum.capsulePrefab:
                    return ObjectPool.instance.startupPools[0].prefab;

                case TestObjectEnum.cubePrefab:
                    return ObjectPool.instance.startupPools[1].prefab;

                case TestObjectEnum.cylinderPrefab:
                    return ObjectPool.instance.startupPools[2].prefab;

                case TestObjectEnum.spherePrefab:
                    return ObjectPool.instance.startupPools[3].prefab;

                case TestObjectEnum.notInsidePrefab:
                    return notInsidePrefab;

                default:
                    return null;
            }
        }

        private List<GameObject> spawnObject;

        public void ShowObjectDetailCount()
        {
            Debug.Log("현재 풀 내부에 생성되어 있는 모든 오브젝트 개수 : " + ObjectPool.CountAllPooled());
            Debug.Log("--------------------------------");
            Debug.Log("현재 풀 안에 생성되어 있는 개수 : " + ObjectPool.CountPooled(GetSelectObject()));
            Debug.Log("현재 풀 밖에 생성되어 있는 개수 : " + ObjectPool.CountSpawned(GetSelectObject()));
        }

        public void BatchingObjectArray()
        {
            for (int i = 0; i < 10; i++)
            {
                ObjectPool.Spawn(GetSelectObject());
            }
        }

        public void RecycleFromSpawnedObject()
        {
            spawnObject = ObjectPool.GetSpawned(GetSelectObject(), spawnObject, false);
            for (int index = 0; index < spawnObject.Count; index++)
            {
                ObjectPool.Recycle(spawnObject[index]);
            }

            spawnObject.Clear();
        }

        public void DestroyObjectInObjectPool()
        {
            spawnObject = ObjectPool.GetPooled(GetSelectObject(), spawnObject, false);
            for (int index = 0; index < spawnObject.Count; index++)
            {
                ObjectPool.Destroy(spawnObject[index]);
            }

            spawnObject.Clear();
        }

        public void DestroyObjectOutObjectPool()
        {
            if (ObjectPool.IsSpawned(GetSelectObject()))
            {
                spawnObject = ObjectPool.GetSpawned(GetSelectObject(), spawnObject, false);
                for (int index = 0; index < spawnObject.Count; index++)
                {
                    ObjectPool.Destroy(spawnObject[index]);
                }

                spawnObject.Clear();
            }
        }

        public void DestroyAllObjectInObjectPool()
        {
            ObjectPool.DestroyPooled(GetSelectObject());
        }
    }
}
