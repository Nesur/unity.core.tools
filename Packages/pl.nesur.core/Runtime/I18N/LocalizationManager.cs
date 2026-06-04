using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nesur.Core.I18N {
	 /// <summary>
    /// Class responsible for loading translations files and loading values to in memory structure.
    /// It also provides methods to get localized strings by key and with parameters.
    /// Files are loaded from Unity Resources directory from I18N subdirectory.
    /// Translation files structure is simple key=value per line. File name should be the same as SystemLanguage value for the language it contains translations for (example: polish.txt)
    /// </summary>
    public class LocalizationManager : Singleton<LocalizationManager> {
        [SerializeField] private string defaultLanguage = "english";
        [SerializeField] private string translationsFolder = "I18N";
        private readonly Dictionary<string, string> _messages = new();


        public string GetLocalizedString(string key) {
            if (!_messages.ContainsKey(key)) {
                Debug.LogWarning($"No localized string found for key: {key}");
                return $"%{key}%";
            }

            string localizedString = _messages[key];
            if (localizedString == null) {
                Debug.LogWarning($"Localized string found for key: {key} but is null");
                return $"%{key}%";
            }

            if (localizedString.Length == 0) {
                Debug.LogWarning($"Localized string found for key: {key} but is empty");
                return $"%{key}%";
            }

            return localizedString;
        }

        public string GetLocalizedStringWithParams(string key, string[] parameters) {
            string localizedString = GetLocalizedString(key);


            for (int index = 0; index < parameters.Length; index++) {
                string parameter = parameters[index];
                localizedString = localizedString.Replace("{" + index + "}", parameter);
            }

            return localizedString;
        }

        protected override void Awake() {
            base.Awake();
            LoadMessagesForLanguage(Application.systemLanguage);
        }

        private void LoadMessagesForLanguage(SystemLanguage systemLanguage) {
            Debug.Log($"loading message for lang: {systemLanguage}");
            TextAsset messagesFile = LoadMessagesFileForLanguage(systemLanguage);
            _messages.Clear();
            string messagesFileText = messagesFile.text;
            string[] lines = messagesFileText.Split("\n");
            foreach (string line in lines) {
                // empty lines are ignored, so you can add empty lines to the translations files to make them more readable
                if (line.Length == 0) {
                    continue;
                }
                string[] lineValues = line.Split("=");
                if (lineValues.Length != 2) {
                    throw new Exception(
                        $"Invalid line: {line}. Line does not contain 2 values.");
                }

                _messages.Add(lineValues[0].Trim(), lineValues[1].Trim());
            }
        }

        private TextAsset LoadMessagesFileForLanguage(SystemLanguage language) {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(translationsFolder);
            foreach (TextAsset textAsset in textAssets) {
                if (textAsset.name.Equals(language.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    return textAsset;
                }
            }
            Debug.Log("Language file not found for language: " + language + ". Falling back to default language: " +
                      defaultLanguage);
            return Resources.Load<TextAsset>($"{translationsFolder}/{defaultLanguage}");
        }
    }
}