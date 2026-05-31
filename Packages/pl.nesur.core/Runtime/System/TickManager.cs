using System.Collections.Generic;

namespace Nesur.Core.System {
    /// <summary>
    /// Manager responsible for executing Update method on MonoBehaviour only on demand.
    /// Used by implementing interface ITickable and registering to the manager, you can control when the OnTick method is called.
    /// </summary>
    public class TickManager : Singleton<TickManager> {
        private readonly List<ITickable> _tickableObjects = new();


        public void RegisterTickable(ITickable tickable) {
            if (!_tickableObjects.Contains(tickable)) {
                _tickableObjects.Add(tickable);
            }
        }

        public void UnregisterTickable(ITickable tickable) {
            if (_tickableObjects.Contains(tickable)) {
                _tickableObjects.Remove(tickable);
            }
        }

        private void FixedUpdate() {
            for (int index = 0; index < _tickableObjects.Count; index++) {
                ITickable tickable = _tickableObjects[index];
                tickable.OnTick();
            }
        }
    }

    public interface ITickable {
        void OnTick();
    }
}