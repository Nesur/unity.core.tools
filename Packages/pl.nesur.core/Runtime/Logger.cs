using UnityEngine;

namespace Nesur.Core {
    public static class Logger {
#if UNITY_EDITOR
        private const bool DEBUG = true;
#endif
#if !UNITY_EDITOR
        private const bool DEBUG = false;
#endif

        public static void Log(string message) {
            if (DEBUG) {
                Debug.Log($"[Logger]: {message}");
            }
        }

        public static void LogError(string message) {
            Debug.LogError($"[Logger]: {message}");
        }
    }
}