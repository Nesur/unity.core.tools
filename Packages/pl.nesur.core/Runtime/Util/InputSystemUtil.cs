using UnityEngine;
using UnityEngine.InputSystem;

namespace Nesur.Core.Util {
    /// <summary>
    /// New input system utility class
    /// </summary>
    public class InputSystemUtil {
        public static bool WasEscapeReleased() {
            if (Keyboard.current != null) {
                return Keyboard.current.escapeKey.wasReleasedThisFrame;
            }

            Debug.Log("No keyboard found. Cannot check if Escape key was released.");
            return false;
        }
        public static bool WasEscapePressed() {
            if (Keyboard.current != null) {
                return Keyboard.current.escapeKey.wasPressedThisFrame;
            }

            Debug.Log("No keyboard found. Cannot check if Escape key was pressed.");
            return false;
        }
    }
}