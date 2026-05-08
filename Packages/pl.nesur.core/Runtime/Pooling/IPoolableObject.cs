using UnityEngine;

namespace Nesur.Core.Pooling {
    public interface IPoolableObject<T> where T : Component {
        void SetPool(ObjectPoolWrapper<T> pool);

        void Release();
    }
}