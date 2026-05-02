using UnityEngine;

namespace Nesur.Core {
    public class SetFps : MonoBehaviour
    {
        [SerializeField] private int targetFps = 30;

        private void Awake()
        {
            Application.targetFrameRate = targetFps;
        }
    }
}