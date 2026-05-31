using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Nesur.Core.Pooling {
    /// <summary>
    /// Implementation supporting object polling 
    /// </summary>
    public class ObjectPoolWrapper<T> where T : Component {
        private readonly IObjectPool<T> _objectPool;
        private readonly T _prefab;

        public ObjectPoolWrapper(T prefab) {
            _objectPool = new ObjectPool<T>(OnCreateObject, OnGetObject, OnReleaseObject);
            _prefab = prefab;
        }

        public T Get(Vector3 newPosition) {
            var poolableObject = Get();
            poolableObject.transform.position = newPosition;
            return poolableObject;
        }

        public T Get(Transform parent) {
            var poolableObject = Get();
            poolableObject.transform.SetParent(parent, false);
            return poolableObject;
        }

        public void Release(T obj) {
            if (obj.gameObject.activeInHierarchy) {
                _objectPool.Release(obj);
            }
        }

        private T Get() {
            var poolableObject = _objectPool.Get();
            if (!poolableObject) {
                poolableObject = OnCreateObject();
            }

            return poolableObject;
        }


        private void OnGetObject(T obj) {
            if (obj != null) {
                obj.gameObject.SetActive(true);
            }
        }

        private void OnReleaseObject(T obj) {
            obj.gameObject.SetActive(false);
        }

        private T OnCreateObject() {
            var newInstance = Object.Instantiate(_prefab, _prefab.transform.position, Quaternion.identity);
            var poolableObject = newInstance.GetComponent<IPoolableObject<T>>();
            if(poolableObject == null) {
                throw new Exception("Pooled object does not implement required interface IPoolableObject.");
            }
            poolableObject.SetPool(this);
            return newInstance;
        }
    }
}
