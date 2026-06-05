using UnityEngine;

namespace Nesur.Core {
    public class LookAtCamera : MonoBehaviour {
        private static readonly Camera mainCamera = Camera.main;
        [SerializeField] private bool lockUpRotation;

        private void Update() {
            Vector3 cameraForwardVector = mainCamera.transform.forward;
            if (lockUpRotation) {
                transform.forward = new Vector3(cameraForwardVector.x, 0, cameraForwardVector.z);
            }
            else {
                transform.forward = cameraForwardVector;
            }
        }
    }
}