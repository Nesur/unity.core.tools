using System.IO;
using Nesur.Core.Util;
using UnityEngine;

namespace Nesur.Core.System.Save {
    public class JsonSaveSystem<T> : ISaveSystem<T> {
        public void Save(T saveObject, string saveName) {
            string json = JsonUtility.ToJson(saveObject);
            byte[] encryptedJson = EncryptionUtil.EncryptUsingAes(json);
            string saveFilePath = GetSaveFilePath(saveName);
            File.WriteAllBytes(saveFilePath, encryptedJson);
        }

        public T Load(string saveName) {
            string saveFilePath = GetSaveFilePath(saveName);
            if (File.Exists(saveFilePath)) {
                byte[] encryptedJson = File.ReadAllBytes(saveFilePath);
                string json = EncryptionUtil.DecryptUsingAes(encryptedJson);
                return JsonUtility.FromJson<T>(json);
            }
            return default(T);
        }
        private string GetSaveFilePath(string saveName) {
            return Application.persistentDataPath + "/" + saveName;
        }
    }
}