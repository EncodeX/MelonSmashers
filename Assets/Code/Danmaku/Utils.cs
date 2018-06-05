using UnityEngine;

namespace Code.Danmaku {
    public class Utils {
        private static int _targetFps = 60;
        public static float frameInterval = 1f / 60;
        public static int pixelPerUnit = 100;
        public static float unitPerPixel = 1f / 100;
        
        public static bool CheckNeedInternalTimer() {
            Application.targetFrameRate = -1;
            if(_targetFps == Application.targetFrameRate)
                return false;
            if(Application.targetFrameRate != -1 && _targetFps > Application.targetFrameRate) {
                Debug.LogError("Application.targetFrameRate is lower than targetFPS, lowering targetFPS to " + _targetFps.ToString());
                _targetFps = Application.targetFrameRate;
                return false;
            }
            return true;
        }

        public static Rect GetGamePlayRect(Camera camera) {
            if (!camera.orthographic)
                Debug.LogError(
                    "No valid orthographic caemra found, please assign a orthographic camera in settings");

            Vector3 pos = camera.transform.position;
            float orthoV = camera.orthographicSize;
            float orthoH = camera.orthographicSize * 3 / 4; // * camera.aspect;
            
            return new Rect(pos.x - orthoH, pos.y - orthoV, orthoH * 2, orthoV * 2);
        }

        public static Rect ExpandRect(Rect rect, float padding) {
            return new Rect(rect.x - padding, rect.y - padding, rect.width + padding * 2, rect.height + padding * 2);
        }
    }
}