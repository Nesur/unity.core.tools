using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        public static bool IsPointerOverUi(Vector2 screenPosition) {
            if (!EventSystem.current) {
                return false;
            }

            PointerEventData pointerEventData = new PointerEventData(EventSystem.current) {
                position = screenPosition
            };
            List<RaycastResult> uiRaycastResults = new();
            EventSystem.current.RaycastAll(pointerEventData, uiRaycastResults);
            return uiRaycastResults.Count > 0;
        }
        
    }
}