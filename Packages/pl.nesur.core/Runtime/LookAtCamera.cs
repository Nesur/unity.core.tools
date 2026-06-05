using Nesur.Core.System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Nesur.Core {
    public class LookAtCamera : MonoBehaviour, ITickable {
        private static readonly Camera mainCamera = Camera.main;
        [SerializeField] private bool alignOnStart = true;
        [SerializeField] private bool lockUpRotation;

        public void OnTick() {
            AlignWithCamera();
        }

        private void Start() {
            if (alignOnStart) {
                AlignWithCamera();
            }
            else {
                TickManager.Instance.RegisterTickable(this);
            }
        }
        
        private void OnDestroy() {
            TickManager.Instance.UnregisterTickable(this);
        }

        private void AlignWithCamera() {
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