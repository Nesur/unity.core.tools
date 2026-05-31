using UnityEngine;

namespace Nesur.Core {
    /// <summary>
    /// Component used to debug empty game objects as sphere in the scene.
    /// </summary>
    [RequireComponent(typeof(Renderer), typeof(MeshFilter))]
    public class EmptyObjectRenderer : MonoBehaviour {
        [SerializeField] private bool hideRendererOnAwake;
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;

        private void Awake() {
            if (hideRendererOnAwake) {
                _meshFilter = GetComponent<MeshFilter>();
                _meshRenderer = GetComponent<MeshRenderer>();
                _meshRenderer.enabled = false;
                _meshFilter.mesh = null;
            }
        }
    }
}