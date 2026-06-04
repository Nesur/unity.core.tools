using TMPro;
using UnityEngine;

namespace Nesur.Core.I18N {
    public class LocalizedText : MonoBehaviour {
        // [Required, SerializeField] private string key;
        [SerializeField] private string key;

        private TextMeshProUGUI _textComponent;

        private void Awake() {
            _textComponent = GetComponent<TextMeshProUGUI>();
        }

        private void Start() {
            UpdateText();
        }

        private void OnEnable() {
            // I18NManager.OnLanguageChanged += UpdateText;
        }

        private void OnDisable() {
            // I18NManager.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText() {
            _textComponent.text = LocalizationManager.Instance.GetLocalizedString(key);
        }
    }
}