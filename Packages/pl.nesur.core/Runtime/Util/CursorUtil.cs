using System;
using UnityEngine;

namespace Nesur.Core.Util {
    public class CursorUtil {
        public static void SetCursor(CursorData cursorData, CursorMode cursorMode) {
            Cursor.SetCursor(cursorData.texture, cursorData.hotspot, cursorMode);
        }

        public static void SetCursorAuto(CursorData cursorData) {
            Cursor.SetCursor(cursorData.texture, cursorData.hotspot, CursorMode.Auto);
        }

        public static void ResetCursor() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        public static void HideCursor() {
            Cursor.visible = false;
        }

        public static void ShowCursor() {
            Cursor.visible = true;
        }

        [Serializable]
        public class CursorData {
            public Texture2D texture;
            public Vector2 hotspot;
        }
    }
}